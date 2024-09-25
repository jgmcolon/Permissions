using Permissions.Application.Abstractions.Messaging;
using Permissions.Application.Permissions.PermissionType.Get;
using Permissions.Domain.Abstractions;
using Permissions.Domain.Repositories.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.PermissionType.GetAll
{

    internal sealed class GetPermissionTypesQueryHandler : IQueryHandler<GetPermissionTypesQuery, IReadOnlyList<PermissionTypeResponse>>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionTypesQueryHandler(
        IPermissionTypeRepository permissionTypeRepository,
        IUnitOfWork unitOfWork)
        {
            _permissionTypeRepository = permissionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<PermissionTypeResponse>>> Handle(GetPermissionTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var list = await _permissionTypeRepository.GetAllAsync(cancellationToken);

                return list!.Select(x => new PermissionTypeResponse { Id = x.KeyId.ToString(), Name = x.Description }).ToList();

            }
            catch (Exception ex)
            {
                return Result.Failure<IReadOnlyList<PermissionTypeResponse>>(new Error("Exception", ex.Message));
            }
        }

    }


}

