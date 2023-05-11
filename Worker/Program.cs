using Domain.Port;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.Extensions.Options;
using ServiceApplication;
using ServicesBus.HandlerAzureServiceBus;
using ServicesBus.HandlerAzureServiceBus.Listener;
using Util.Common;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        
        #region CosmosDB for mongo
        services.Configure<ConfigurateCosmosDB>(option =>
        {
            option.ConnectionString = "mongodb://865f12f0-e213-49e6-97b8-9d3147b80102:cU91n1lK7QiPQmWAYyNzV6xIM3O1aCotYlepZepXE4sa76G4P3HImaeHMRdwh0wfv7s6rVPN74fmACDbIxnAmQ==@865f12f0-e213-49e6-97b8-9d3147b80102.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@865f12f0-e213-49e6-97b8-9d3147b80102@";
            option.DatabaseName = "11a79623-f386-4172-a8bc-761f4e7d40e4";
        });

        services.AddSingleton<IConfigurateCosmosDB>(sp => sp.GetRequiredService<IOptions<ConfigurateCosmosDB>>().Value);

        services.AddSingleton<IMainContextCosmos, MainContextCosmosDB>();
        #endregion


        services.AddTransient<IServicesListenerHandler, ServicesListenerHandler>();
        services.AddTransient<IServicesListener2Handler, ServicesListener2Handler>();
    })
    .Build()
    .RunAsync();

