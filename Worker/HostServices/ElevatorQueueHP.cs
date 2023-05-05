using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Common;
using ServiceApplication;
using ServicesBus.HandlerAzureServiceBus;
using ServicesBus.HandlerAzureServiceBus.Listener;
using Util.Common;
using UtilNuget.Enum;

namespace Worker.initial;

public class ElevatorQueueHP : IHostedService
{
    private readonly ILogger<ElevatorQueueHP> _logger;
    private readonly IServicesListenerHandler _servicesListenerHandler;
    private readonly IElevatorService _elevatorService;
    private readonly IElevatorMovementService _elevatorMovementService;

    public ElevatorQueueHP(ILogger<ElevatorQueueHP> logger, IServicesListenerHandler servicesListenerHandler,
        IElevatorService elevatorService,
        IElevatorMovementService elevatorMovementService)
    {
        _logger = logger;
        _servicesListenerHandler = servicesListenerHandler;
        _elevatorService = elevatorService;
        _elevatorMovementService = elevatorMovementService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _servicesListenerHandler.EventBusiness = ProcessMessage;
        await _servicesListenerHandler.DeQueueAuto("queuehighpriority");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected async Task ProcessMessage(EventQueue eventQueue)
    {
        try
        {
            //Elevator status
            var elevator = await _elevatorService.SearchModel("Code", eventQueue.CodeElevator);
            if (elevator.Status == States.Process.ToString())
            {
                _servicesListenerHandler.DeQueue = false;
            }
            else
            {
                //Elevator
                elevator.Status = States.Process.ToString();
                _elevatorService.UpdateModel(elevator);

                //Movement
                var movement = await _elevatorMovementService.SearchModel("Code", eventQueue.CodeMovement);

                //Move Elevator
                var floors = Math.Abs(elevator.LastFloor - eventQueue.Floor);
                Console.WriteLine($"starting: Priority 1 -- Destination floor: {eventQueue.Floor}");
                Console.WriteLine($"moving....");
                await _elevatorService.MoveToFloor(floors,_elevatorService.IsUp(elevator.LastFloor,eventQueue.Floor), elevator);
                Console.WriteLine($"Finished: Priority 1 -- We arrived on the floor: {eventQueue.Floor} -- Floors traveled: {floors}");

                //Elevator
                elevator.Status = States.Complete.ToString();
                elevator.LastFloor = eventQueue.Floor;
                await _elevatorService.UpdateModel(elevator);

                //Movement
                movement.Status = States.Complete.ToString();
                await _elevatorMovementService.UpdateModel(movement);
                _servicesListenerHandler.DeQueue = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

