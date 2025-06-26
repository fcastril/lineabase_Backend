using Azure.Messaging.ServiceBus;
using BlobStorageMtow;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceApplication.Base;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using ServiceApplication.Events;
using ServiceApplication.Interface;
using ServiceApplication.Models.Auth.Validator;
using ServiceApplication.Models.Github.Service;
using ServiceApplication.Port;
using ServiceApplication.Validator;
using ServiceBus.HandlerAzureServiceBus;
using ServicesBus.HandlerAzureServiceBus;
using ServicesBus.HandlerAzureServiceBus.Listener;
using System.Collections.Generic;
using Util.Common;

namespace ServiceApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionsQueue(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IServicesBusHandler, ServiceSenderHandler>();
            services.AddTransient<IServicesListenerHandler, ServicesListenerHandler>();
            
            services.AddScoped<IGithubService, GithubService>();

            services.AddSingleton<IMessageSender<MessageQueue>>(x => new EventsAsyncSender<MessageQueue>(configuration, x.GetRequiredService<ITableStorage<Transactions>>(), x.GetRequiredService<IMediator>()));
            services.AddSingleton<IMessageSender<CommGeneric>>(x => new EventsAsyncSender<CommGeneric>(configuration, x.GetRequiredService<ITableStorage<Transactions>>(), x.GetRequiredService<IMediator>()));

            services.AddSingleton(provider =>
            {
                var connectionString = configuration.GetSection("ServicesBus:listen").Value;
                return new ServiceBusClient(connectionString);
            });

            services.AddSingleton(provider =>
            {
                var client = provider.GetRequiredService<ServiceBusClient>();
                var queueName = configuration.GetSection("ServicesBus:queueName").Value;
                return client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
            });

            services.AddHostedService<EventsAsyncListener>();

            return services;
        }

        public static IServiceCollection AddDependencyInjectionsApplications(this IServiceCollection services)
        {
            services.AddScoped<IOverviewMigrationService, OverviewMigrationService>();

            services.AddScoped<IValidator<RolDto>, RolValidator>();
            services.AddScoped<IValidator<UserDto>, SecurityValidator>();

            services.AddScoped<IValidator<AreaDto>, AreaValidator>();
            services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();
            services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();

            services.AddScoped<IValidator<DiscoveryDto>, DiscoveryValidator>();
            services.AddScoped<IValidator<CurrentToolDto>, CurrentToolValidator>();
            services.AddScoped<IValidator<ProblemDetailsDto>, ProblemDetailsValidator>();


            services.AddScoped<IValidator<FrecuencyDto>, FrecuencyValidator>();
            services.AddScoped<IValidator<ProblemDto>, ProblemValidator>();
            services.AddScoped<IValidator<SectorDto>, SectorValidator>();
            services.AddScoped<IValidator<ToolDto>, ToolValidator>();

            services.AddScoped<IValidator<BenefitDto>, BenefitValidator>();
            services.AddScoped<IValidator<ExpectedBenefitDto>, ExpectedBenefitValidator>();
            services.AddScoped<IValidator<PromptDto>, PromptValidator>();
            services.AddScoped<IValidator<ConnectToolDto>, ConnectToolValidator>();
            services.AddScoped<IValidator<UserMigrationDto>, UserMigrationValidator>();
            services.AddScoped<IValidator<RepositoryMigrationDto>, RepositoryMigrationValidator>();
            services.AddScoped<IValidator<RepositoryAnalizeGenAIDto>, RepositoryAnalizeGenAIValidator>();
            services.AddScoped<IValidator<PackageMigrationDto>, PackageMigrationValidator>();
            services.AddScoped<IValidator<SecurityMigrationDto>, SecurityMigrationValidator>();
            services.AddScoped<IValidator<PipelineMigrationDto>, PipelineMigrationValidator>();

            services.AddScoped<IValidator<EstimationOverviewDto>, EstimationOverviewValidator>();

            return services;
        }

        public static IServiceCollection AddMediatrDependecyInjection(this IServiceCollection services)
        {

            services.RegisterMediatrAbstractService<SecurityService, UserDto, User, ISecurityService>();
            services.RegisterMediatrAbstractService<RolService, RolDto, Rol, IRolService>();

            services.RegisterMediatrAbstractService<AreaService, AreaDto, Area, IAreaService>();
            services.RegisterMediatrAbstractService<CategoryService, CategoryDto, Category, ICategoryService>();
            services.RegisterMediatrAbstractService<CustomerService, CustomerDto, Customer, ICustomerService>();
            services.RegisterMediatrAbstractService<DiscoveryService, DiscoveryDto, Discovery, IDiscoveryService>();
            services.RegisterMediatrAbstractService<CurrentToolService, CurrentToolDto, CurrentTool, ICurrentToolService>();
            services.RegisterMediatrAbstractService<ProblemDetailsService, ProblemDetailsDto, ProblemDetails, IProblemDetailsService>();

            services.RegisterMediatrAbstractService<FrecuencyService, FrecuencyDto, Frecuency, IFrecuencyService>();
            services.RegisterMediatrAbstractService<ProblemService, ProblemDto, Problem, IProblemService>();
            services.RegisterMediatrAbstractService<SectorService, SectorDto, Sector, ISectorService>();
            services.RegisterMediatrAbstractService<ToolService, ToolDto, Tool, IToolService>();

            services.RegisterMediatrAbstractService<BenefitService, BenefitDto, Benefit, IBenefitService>();
            services.RegisterMediatrAbstractService<ExpectedBenefitService, ExpectedBenefitDto, ExpectedBenefit, IExpectedBenefitService>();
            services.RegisterMediatrAbstractService<PromptService, PromptDto, Prompt, IPromptService>();
            services.RegisterMediatrAbstractService<ConnectToolService, ConnectToolDto, ConnectTool, IConnectToolService>();
            services.RegisterMediatrAbstractService<UserMigrationService, UserMigrationDto, UserMigration, IUserMigrationService>();
            services.RegisterMediatrAbstractService<RepositoryMigrationService, RepositoryMigrationDto, RepositoryMigration, IRepositoryMigrationService>();
            services.RegisterMediatrAbstractService<RepositoryAnalizeGenAIService, RepositoryAnalizeGenAIDto, RepositoryAnalizeGenAI, IRepositoryAnalizeGenAIService>();
            services.RegisterMediatrAbstractService<PackageMigrationService, PackageMigrationDto, PackageMigration, IPackageMigrationService>();
            services.RegisterMediatrAbstractService<SecurityMigrationService, SecurityMigrationDto, SecurityMigration, ISecurityMigrationService>();
            services.RegisterMediatrAbstractService<PipelineMigrationService, PipelineMigrationDto, PipelineMigration, IPipelineMigrationService>();

            services.RegisterMediatrAbstractService<EstimationOverviewService, EstimationOverviewDto, EstimationOverview, IEstimationOverviewService>();

            return services;
        }

        public static IServiceCollection AddMapperDependencyInjection(this IServiceCollection services)
        {
            return services;
        }

        public static void RegisterMediatrAbstractService<Service, DTO, ENT, TImplementation>(this IServiceCollection services)
            where Service : BaseServiceApplication<ENT, DTO>
            where DTO : class, new()
            where ENT : class, new()
            where TImplementation : IBaseServiceApplication<ENT, DTO>
        {
            services.AddScoped(typeof(TImplementation), typeof(Service));
            services.AddScoped(typeof(IBaseServiceApplication<ENT, DTO>), typeof(Service));

            services.AddMediatR(typeof(CreateAsyncCommandHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<CreateAsyncCommand<ENT, DTO>, DTO>), typeof(CreateAsyncCommandHandler<ENT, DTO>));
            services.AddMediatR(typeof(UpdateAsyncCommandHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<UpdateAsyncCommand<ENT, DTO>, DTO>), typeof(UpdateAsyncCommandHandler<ENT, DTO>));
            services.AddMediatR(typeof(DeleteAsyncCommandHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<DeleteAsyncCommand<ENT, DTO>, bool>), typeof(DeleteAsyncCommandHandler<ENT, DTO>));
            services.AddMediatR(typeof(ToListAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<ToListAsyncQuery<ENT, DTO>, List<DTO>>), typeof(ToListAsyncQueryHandler<ENT, DTO>));
            services.AddMediatR(typeof(PaginateAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<PaginateAsyncQuery<ENT, DTO>, Paginate<DTO>>), typeof(PaginateAsyncQueryHandler<ENT, DTO>));
            services.AddMediatR(typeof(PaginateWithPageAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<PaginateWithPageAsyncQuery<ENT, DTO>, Paginate<DTO>>), typeof(PaginateWithPageAsyncQueryHandler<ENT, DTO>));
            services.AddMediatR(typeof(SearchAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<SearchAsyncQuery<ENT, DTO>, DTO>), typeof(SearchAsyncQueryHandler<ENT, DTO>));
            services.AddMediatR(typeof(SearchListAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<SearchListAsyncQuery<ENT, DTO>, List<DTO>>), typeof(SearchListAsyncQueryHandler<ENT, DTO>));
            services.AddMediatR(typeof(GetByIdAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<GetByIdAsyncQuery<ENT, DTO>, DTO>), typeof(GetByIdAsyncQueryHandler<ENT, DTO>));

            services.AddMediatR(typeof(CreateManyAsyncCommandHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<CreateManyAsyncCommand<ENT, DTO>, bool>), typeof(CreateManyAsyncCommandHandler<ENT, DTO>));
        }

        public static void RegisterMediatrCustom(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LoginAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<LoginAsyncQuery, Login>), typeof(LoginAsyncQueryHandler));

            services.AddMediatR(typeof(CreateCurrentToolAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<CreateCurrentToolAsyncCommand, CurrentToolDto>), typeof(CreateCurrentToolAsyncCommandHandler));

            services.AddMediatR(typeof(UpdateCurrentToolAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<UpdateCurrentToolAsyncCommand, CurrentToolDto>), typeof(UpdateCurrentToolAsyncCommandHandler));

            services.AddMediatR(typeof(ToListCurrentToolAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<ToListCurrentToolAsyncQuery, List<CurrentToolDto>>), typeof(ToListCurrentToolAsyncQueryHandler));

            services.AddMediatR(typeof(GetByIdCurrentToolAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByIdCurrentToolAsyncQuery, CurrentToolDto>), typeof(GetByIdCurrentToolAsyncQueryHandler));

            services.AddMediatR(typeof(DeleteCurrentToolAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteCurrentToolAsyncCommand, bool>), typeof(DeleteCurrentToolAsyncCommandHandler));

            services.AddMediatR(typeof(PaginateCurrentToolAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<PaginateCurrentToolAsyncQuery, Paginate<CurrentToolDto>>), typeof(PaginateCurrentToolAsyncQueryHandler));


            services.AddMediatR(typeof(CreateProblemDetailsAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<CreateProblemDetailsAsyncCommand, ProblemDetailsDto>), typeof(CreateProblemDetailsAsyncCommandHandler));

            services.AddMediatR(typeof(UpdateCurrentToolAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<UpdateCurrentToolAsyncCommand, CurrentToolDto>), typeof(UpdateCurrentToolAsyncCommandHandler));

            services.AddMediatR(typeof(ToListProblemDetailsAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<ToListProblemDetailsAsyncQuery, List<ProblemDetailsDto>>), typeof(ToListProblemDetailsAsyncQueryHandler));

            services.AddMediatR(typeof(GetByIdProblemDetailsAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByIdProblemDetailsAsyncQuery, ProblemDetailsDto>), typeof(GetByIdProblemDetailsAsyncQueryHandler));

            services.AddMediatR(typeof(DeleteProblemDetailsAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteProblemDetailsAsyncCommand, bool>), typeof(DeleteProblemDetailsAsyncCommandHandler));

            services.AddMediatR(typeof(PaginateProblemDetailsAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<PaginateProblemDetailsAsyncQuery, Paginate<ProblemDetailsDto>>), typeof(PaginateProblemDetailsAsyncQueryHandler));


            services.AddMediatR(typeof(GetByIdCustomerAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByIdCustomerAsyncQuery, DiscoveryDto>), typeof(GetByIdCustomerAsyncQueryHandler));

            services.AddMediatR(typeof(ToListBenefitByDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<ToListBenefitByDiscoveryIdAsyncQuery, List<BenefitDto>>), typeof(ToListBenefitByDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(PaginateBenefitAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<PaginateBenefitAsyncQuery, Paginate<BenefitDto>>), typeof(PaginateBenefitAsyncQueryHandler));

            services.AddMediatR(typeof(GetByNamePromptAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByNamePromptAsyncQuery, PromptDto>), typeof(GetByNamePromptAsyncQueryHandler));

            services.AddMediatR(typeof(ToListExpectedBenefitByDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<ToListExpectedBenefitByDiscoveryIdAsyncQuery, List<ExpectedBenefitDto>>), typeof(ToListExpectedBenefitByDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(GetConnectToolByDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetConnectToolByDiscoveryIdAsyncQuery, ConnectToolDto>), typeof(GetConnectToolByDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(GetByUserMigrationDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByUserMigrationDiscoveryIdAsyncQuery, List<UserMigrationDto>>), typeof(GetByUserMigrationDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(FilteredRoleActivityUserMigrationQueryHandler));
            services.AddScoped(typeof(IRequestHandler<FilteredRoleActivityUserMigrationQuery, List<UserMigrationDto>>), typeof(FilteredRoleActivityUserMigrationQueryHandler));

            services.AddMediatR(typeof(FilteredNameEmailUserMigrationQueryHandler));
            services.AddScoped(typeof(IRequestHandler<FilteredNameEmailUserMigrationQuery, List<UserMigrationDto>>), typeof(FilteredNameEmailUserMigrationQueryHandler));

            services.AddMediatR(typeof(GetByNameRepositoryMigrationAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByNameRepositoryMigrationAsyncQuery, RepositoryMigrationDto>), typeof(GetByNameRepositoryMigrationAsyncQueryHandler));

            services.AddMediatR(typeof(FilterStatusLastUpdateRepositoryMigrationQueryHandler));
            services.AddScoped(typeof(IRequestHandler<FilterStatusLastUpdateRepositoryMigrationQuery, List<RepositoryMigrationDto>>), typeof(FilterStatusLastUpdateRepositoryMigrationQueryHandler));

            services.AddMediatR(typeof(DeleteBenefitsByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteBenefitsByDiscoveryIdAsyncCommand, bool>), typeof(DeleteBenefitsByDiscoveryIdAsyncCommandHandler));

            services.AddMediatR(typeof(DeleteExpectedBenefitsByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteExpectedBenefitsByDiscoveryIdAsyncCommand, bool>), typeof(DeleteExpectedBenefitsByDiscoveryIdAsyncCommandHandler));

            services.AddMediatR(typeof(GetByRepositoryMigrationDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByRepositoryMigrationDiscoveryIdAsyncQuery, List<RepositoryMigrationDto>>), typeof(GetByRepositoryMigrationDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(FilterPackageMigrationQueryHandler));
            services.AddScoped(typeof(IRequestHandler<FilterPackageMigrationQuery, List<PackageMigrationDto>>), typeof(FilterPackageMigrationQueryHandler));

            services.AddMediatR(typeof(GetBySecurityMigrationDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetBySecurityMigrationDiscoveryIdAsyncQuery, List<SecurityMigrationDto>>), typeof(GetBySecurityMigrationDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(GetByPipelineMigrationDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByPipelineMigrationDiscoveryIdAsyncQuery, List<PipelineMigrationDto>>), typeof(GetByPipelineMigrationDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(GetGithubAppTokenQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetGithubAppTokenQuery, GithubAppResponse>), typeof(GetGithubAppTokenQueryHandler));

            services.AddMediatR(typeof(GetGithubAppRefreshTokenQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetGithubAppRefreshTokenQuery, GithubAppResponse>), typeof(GetGithubAppRefreshTokenQueryHandler));

            services.AddMediatR(typeof(GetGithubAppEmailsQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetGithubAppEmailsQuery, List<GithubAppEmailResponse>>), typeof(GetGithubAppEmailsQueryHandler));

            services.AddMediatR(typeof(GetByEstimationOverviewDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByEstimationOverviewDiscoveryIdAsyncQuery, List<EstimationOverviewDto>>), typeof(GetByEstimationOverviewDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(DeleteEstimationOverviewByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteEstimationOverviewByDiscoveryIdAsyncCommand, bool>), typeof(DeleteEstimationOverviewByDiscoveryIdAsyncCommandHandler));

            services.AddMediatR(typeof(ToListRepositoryAnalizeGenAIRepositoryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<ToListRepositoryAnalizeGenAIRepositoryIdAsyncQuery, List<RepositoryAnalizeGenAIDto>>), typeof(ToListRepositoryAnalizeGenAIRepositoryIdAsyncQueryHandler));

            services.AddMediatR(typeof(DeleteRepositoryAnalizeGenAIByRepositoryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteRepositoryAnalizeGenAIRepositoryIdAsyncCommand, bool>), typeof(DeleteRepositoryAnalizeGenAIByRepositoryIdAsyncCommandHandler));

            services.AddMediatR(typeof(GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQuery, List<RepositoryAnalizeGenAIDto>>), typeof(GetByRepositoryAnalizeGenAIDiscoveryIdAsyncQueryHandler));

            services.AddMediatR(typeof(DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommand, bool>), typeof(DeleteRepositoryAnalizeGenAIByDiscoveryIdAsyncCommandHandler));

            services.AddMediatR(typeof(DeleteUserMigrationByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteUserMigrationByDiscoveryIdAsyncCommand, bool>), typeof(DeleteUserMigrationByDiscoveryIdAsyncCommandHandler));

            services.AddMediatR(typeof(DeleteSecurityMigrationByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteSecurityMigrationByDiscoveryIdAsyncCommand, bool>), typeof(DeleteSecurityMigrationByDiscoveryIdAsyncCommandHandler));

            services.AddMediatR(typeof(DeleteRepositoryMigrationByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeleteRepositoryMigrationByDiscoveryIdAsyncCommand, bool>), typeof(DeleteRepositoryMigrationByDiscoveryIdAsyncCommandHandler));

            services.AddMediatR(typeof(DeletePipelineMigrationByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeletePipelineMigrationByDiscoveryIdAsyncCommand, bool>), typeof(DeletePipelineMigrationByDiscoveryIdAsyncCommandHandler));

            services.AddMediatR(typeof(DeletePackageMigrationByDiscoveryIdAsyncCommandHandler));
            services.AddScoped(typeof(IRequestHandler<DeletePackageMigrationByDiscoveryIdAsyncCommand, bool>), typeof(DeletePackageMigrationByDiscoveryIdAsyncCommandHandler));


        }
    }
}
