using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record FilteredNameEmailUserMigrationQuery(string search) : IRequest<List<UserMigrationDto>>;

    public class FilteredNameEmailUserMigrationQueryHandler : IRequestHandler<FilteredNameEmailUserMigrationQuery, List<UserMigrationDto>>
    {
        protected readonly IBaseServiceApplication<UserMigration, UserMigrationDto> _implementation;

        public FilteredNameEmailUserMigrationQueryHandler(IBaseServiceApplication<UserMigration, UserMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<UserMigrationDto>> Handle(FilteredNameEmailUserMigrationQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.search))
            {
                return await _implementation.TolistModel();
            }

            return await _implementation.ToListModelBy(u => u.Name.ToLower().Contains(request.search.ToLower()) || u.Email.ToLower().Contains(request.search.ToLower()));
        }
    }
}
