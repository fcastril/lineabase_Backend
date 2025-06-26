using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record FilterStatusLastUpdateRepositoryMigrationQuery(bool isDisable, DateTime lastUpdate) : IRequest<List<RepositoryMigrationDto>>;

    public class FilterStatusLastUpdateRepositoryMigrationQueryHandler : IRequestHandler<FilterStatusLastUpdateRepositoryMigrationQuery, List<RepositoryMigrationDto>>
    {
        protected readonly IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto> _implementation;

        public FilterStatusLastUpdateRepositoryMigrationQueryHandler(IBaseServiceApplication<RepositoryMigration, RepositoryMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<RepositoryMigrationDto>> Handle(FilterStatusLastUpdateRepositoryMigrationQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.isDisable.ToString()) && string.IsNullOrWhiteSpace(request.lastUpdate.ToString()))
            {
                return await _implementation.TolistModel();
            }

            return await _implementation.ToListModelBy(u => u.IsDisabled == request.isDisable && (request.lastUpdate > u.LastCommit && request.lastUpdate < DateTime.Now));
        }
    }
}
