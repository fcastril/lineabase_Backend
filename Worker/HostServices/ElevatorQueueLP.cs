using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Common;
using ServiceApplication;
using ServicesBus.HandlerAzureServiceBus;
using ServicesBus.HandlerAzureServiceBus.Listener;
using Util.Common;
using UtilNuget.Enum;

namespace Worker.initial;

public class ElevatorQueueLP : IHostedService
{
    private readonly ILogger<ElevatorQueueLP> _logger;
    private readonly IServicesListener2Handler _servicesListenerHandler;
    private readonly IElevatorService _elevatorService;
    private readonly IElevatorMovementService _elevatorMovementService;

    public ElevatorQueueLP(ILogger<ElevatorQueueLP> logger, IServicesListener2Handler servicesListenerHandler,
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
        await _servicesListenerHandler.DeQueueAuto("queuelowpriority");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected async Task ProcessMessage(EventQueue eventQueue)
    {
        try
        {
            var elevator = await _elevatorService.SearchModel("Code", eventQueue.CodeElevator);
            var movements = await _elevatorMovementService.ToListModelDtoBy(f=>f.Status==States.Active.ToString() && f.Priority==1);
            if (movements != null && movements.Count>0)
            {
                Console.WriteLine($"starting: Priority 2 --Occupied by priority 1");
                await Task.Delay(2000);
                _servicesListenerHandler.DeQueue = false;
            }
            else
            {
                elevator.Status = States.Process.ToString();
                _elevatorService.UpdateModel(elevator);

                var floors = Math.Abs(elevator.LastFloor - eventQueue.Floor);
                Console.WriteLine($"starting: Priority 2 -- Destination floor: {eventQueue.Floor}");
                Console.WriteLine($"moving....");
                await _elevatorService.MoveToFloor(floors, _elevatorService.IsUp(elevator.LastFloor, eventQueue.Floor), elevator);
                Console.WriteLine($"Finished: Priority 2 -- We arrived on the floor: {eventQueue.Floor}  -- Floors traveled: {floors}");
                //Elevator
                elevator.Status = States.Complete.ToString();
                elevator.LastFloor = eventQueue.Floor;
                await _elevatorService.UpdateModel(elevator);

                //Movement
                var movement = await _elevatorMovementService.SearchModel("Code", eventQueue.CodeMovement);

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

