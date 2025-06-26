using Azure.Messaging.ServiceBus;
using BlobStorageMtow;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using ServiceApplication.Port;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.Events
{
    public class EventsAsyncSender<T> : IMessageSender<T>
        where T: MessageQueue
    {
        private readonly IConfiguration _configuration;
        private readonly ITableStorage<Transactions> _tableStorage;
        private readonly IMediator _mediator;

        public EventsAsyncSender(IConfiguration _configuration, ITableStorage<Transactions> tableStorage, IMediator mediator)
        {
            this._configuration = _configuration;
            this._tableStorage = tableStorage;
            this._mediator = mediator;
        }

        public async Task SendCommAsync(T message, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var client = new ServiceBusClient(_configuration.GetSection("ServicesBus:send").Value);
                ServiceBusSender sender = client.CreateSender(_configuration.GetSection("ServicesBus:queueName").Value);
                string jsonMessage = JsonSerializer.Serialize(message);
                ServiceBusMessage serviceBusMessage = new(jsonMessage);

                await CreateLog($"SendMessageQueue - {_configuration["ServicesBus:queueName"]}", "SendMessage Ok", "IQueue", Guid.NewGuid().ToString());
                await sender.SendMessageAsync(serviceBusMessage, cancellationToken);
            }
            catch (Exception ex) when (ex is ArgumentException ||
                        ex is ServiceBusException || 
                        ex is UnauthorizedAccessException)
            {
                throw;
            }
        }

        public async Task SendAsync(T message, CancellationToken cancellationToken = default)
        {
            try
            {
                await SendCommAsync(message, cancellationToken);
                await UpdateDiscoveryStatusBenefitAsync(message.DiscoveryId, StatusBenefit.MessageSentToQueue);
            }
            catch (Exception ex) when (ex is ArgumentException ||
                        ex is ServiceBusException ||
                        ex is UnauthorizedAccessException)
            {
                await UpdateDiscoveryStatusBenefitAsync(message.DiscoveryId, StatusBenefit.ErrorSendingMessageToQueue);
                throw;
            }
        }

        public async Task CreateLog(object input, object output, string CategoryOrigin, string codeClient = null)
        {
            await _tableStorage.InsertData(new Transactions
            {
                Input = JsonSerializer.Serialize(input),
                Output = JsonSerializer.Serialize(output),
                subdomain = Esubdomain.IORCH.ToString(),
                OriginCategory = CategoryOrigin,
                ClientId = codeClient,
                PartitionKey = typeof(EventsAsyncSender<T>).Name
            });
        }

        private async Task UpdateDiscoveryStatusBenefitAsync(string discoveryId, StatusBenefit statusBenefit)
        {
            DiscoveryDto discovery = await _mediator.Send(new GetByIdAsyncQuery<Discovery, DiscoveryDto>(discoveryId));
            discovery.StatusBenefit = statusBenefit.ToString();
            await _mediator.Send(new UpdateAsyncCommand<Discovery, DiscoveryDto>(discovery));
        }
    }
}
