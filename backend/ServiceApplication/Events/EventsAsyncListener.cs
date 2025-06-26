using Azure.Messaging.ServiceBus;
using BlobStorageMtow;
using Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceApplication.Functions;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.Events
{
    public class EventsAsyncListener : IHostedService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ITableStorage<Transactions> _tableStorage;
        private readonly ServiceBusProcessor _processor;

        public EventsAsyncListener(
            IConfiguration configuration,
            ITableStorage<Transactions> tableStorage,
            ServiceBusProcessor processor)
        {
            _configuration = configuration;
            _tableStorage = tableStorage;
            _processor = processor;

            _processor.ProcessMessageAsync += ProcessMessagesAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _processor.StartProcessingAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync(cancellationToken);
            await _processor.CloseAsync(cancellationToken);
        }

        public void Dispose()
        {
            _processor?.DisposeAsync().GetAwaiter().GetResult();
            GC.SuppressFinalize(this);
        }

        public async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
        {
            try
            {
                var message = args.Message.Body.ToObjectFromJson<CommGeneric>();

                if (message.Tool == Tools.GenAIBenefits.ToString() ||
                    message.Tool == Tools.GenAIOverview.ToString() ||
                    message.Tool == Tools.GenAIRepository.ToString())
                {
                    string discoveryId = message.DiscoveryId;
                    var httpClient = new HttpClient();
                    GenAI genAI = new(httpClient, _configuration);
                    bool response = await genAI.SendAsync(discoveryId, message.Tool);

                    await args.CompleteMessageAsync(args.Message);
                }
            }
            catch (Exception ex)
            {
                await CreateLog(ex, $"Error processing message: {ex.Message}", "Error");
            }
        }

        public Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            CreateLog(args.Exception, $"Error source: {args.ErrorSource}, Entity: {args.EntityPath}", "Error").Wait();
            return Task.CompletedTask;
        }

        private async Task CreateLog(object input, object output, string categoryOrigin, string codeClient = null)
        {
            await _tableStorage.InsertData(new Transactions
            {
                Input = JsonSerializer.Serialize(input is Exception ex ? ex.Message : input),
                Output = JsonSerializer.Serialize(output),
                subdomain = Esubdomain.IORCH.ToString(),
                OriginCategory = categoryOrigin,
                ClientId = codeClient,
                PartitionKey = typeof(EventsAsyncListener).Name
            });
        }
    }
}
