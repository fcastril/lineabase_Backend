using System.Collections.Generic;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceApplication.Base;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Validator;
using Util.Common;

namespace ServiceApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionsApplications(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RolDto>, RolValidator>();
            services.AddScoped<IValidator<UserDto>, UserValidator>();
            return services;
        }

        public static IServiceCollection AddMediatrDependecyInjection(this IServiceCollection services)
        {

            services.RegisterMediatrCustom();

            services.RegisterMediatrAbstractService<UserService, UserDto, User, IUserService>();
            services.RegisterMediatrAbstractService<RolService, RolDto, Rol, IRolService>();

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
            services.AddScoped(typeof(IRequestHandler<CreateAsyncCommand<ENT, DTO>, DTO>),typeof(CreateAsyncCommandHandler<ENT, DTO>));
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

        }

        public static void RegisterMediatrCustom(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LoginAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<LoginAsyncQuery, Login>), typeof(LoginAsyncQueryHandler));

        }
    }
}
