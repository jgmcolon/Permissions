using Permissions.Application.Abstractions.Messaging;
using Permissions.Application.Extensions;
using Permissions.Application.Permissions.PermissionType.Delete;
using Permissions.Domain.Abstractions;
using Permissions.Domain.Abstractions.Messaging;
using Permissions.Domain.Repositories.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.PermissionType.Get
{

    internal sealed class GetPermissionTypeQueryHandler : IQueryHandler<GetPermissionTypeQuery, PermissionTypeResponse>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionTypeQueryHandler(
        IPermissionTypeRepository permissionTypeRepository,
        IUnitOfWork unitOfWork)
        {
            _permissionTypeRepository = permissionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PermissionTypeResponse>> Handle(GetPermissionTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {

                if (!request.PermissionTypeId.IsGuid())
                {
                    return Result.Failure<PermissionTypeResponse> (new Error("permissionType", $"{request.PermissionTypeId} not is GUID"));
                }

                var permissionType = await _permissionTypeRepository.GetByIdAsync(request.PermissionTypeId.ToGuid(), cancellationToken);

                if (permissionType is null)
                {
                    return Result.Failure<PermissionTypeResponse>(new Error("permissionType", $"Permission Type {request.PermissionTypeId} Not Found"));
                }

                return new PermissionTypeResponse { 
                     Id = permissionType.KeyId.ToString(),
                      Name = permissionType.Description
                };

            }
            catch (Exception ex)
            {
                return Result.Failure<PermissionTypeResponse>(new Error("Exception", ex.Message));
            }
        }

    }


}
