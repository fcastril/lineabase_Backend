using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApplication.CQRS
{
    public record FilterPackageMigrationQuery(string type = null, bool? isDeleted = null, DateTime? lastUpdate = null) : IRequest<List<PackageMigrationDto>>;

    public class FilterPackageMigrationQueryHandler : IRequestHandler<FilterPackageMigrationQuery, List<PackageMigrationDto>>
    {
        protected readonly IBaseServiceApplication<PackageMigration, PackageMigrationDto> _implementation;

        public FilterPackageMigrationQueryHandler(IBaseServiceApplication<PackageMigration, PackageMigrationDto> implementation)
        {
            _implementation = implementation;
        }

        public async Task<List<PackageMigrationDto>> Handle(FilterPackageMigrationQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<PackageMigration, bool>> expression = null;
            
            if (!string.IsNullOrEmpty(request.type))
            {
                expression = x => x.Type.ToLower().Contains(request.type.ToLower());
            }

            if (request.isDeleted.HasValue)
            {
                expression = x => x.IsDeleted == request.isDeleted;
            }

            if (request.lastUpdate.HasValue)
            {
                expression = x => x.LastUpdate == request.lastUpdate;
            }

            return expression != null ? await _implementation.ToListModelBy(expression) : await _implementation.TolistModel();
        }
    }
}
