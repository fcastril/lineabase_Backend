using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record FilteredRoleActivityUserMigrationQuery(string role, int inactivityDays) : IRequest<List<UserMigrationDto>>;

    public class FilteredRoleActivityUserMigrationQueryHandler : IRequestHandler<FilteredRoleActivityUserMigrationQuery, List<UserMigrationDto>>
    {
        protected readonly IBaseServiceApplication<UserMigration, UserMigrationDto> _implementation;

        public FilteredRoleActivityUserMigrationQueryHandler(IBaseServiceApplication<UserMigration, UserMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<UserMigrationDto>> Handle(FilteredRoleActivityUserMigrationQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.role) && string.IsNullOrEmpty(request.inactivityDays.ToString()))
            {
                return await _implementation.TolistModel();
            }

            return await _implementation.ToListModelBy(u => u.Role.ToLower().Contains(request.role.ToLower()) && u.LastAccess < DateTime.Now.AddDays(request.inactivityDays*-1));
        }
    }
}
