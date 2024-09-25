using Permissions.Application.Abstractions.Messaging;
using Permissions.Application.Extensions;
using Permissions.Application.Permissions.Permissions;
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

    internal sealed class GetPermissionQueryHandler : IQueryHandler<GetPermissionQuery, PermissionResponse>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionQueryHandler(
        IPermissionRepository permissionRepository,
        IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PermissionResponse>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {

                if (!request.PermissionId.IsGuid())
                {
                    return Result.Failure<PermissionResponse>(new Error("Permission", $"{request.PermissionId} not is GUID"));
                }

                var permission = await _permissionRepository.GetByIdAsync(request.PermissionId.ToGuid(), cancellationToken);

                if (permission is null)
                {
                    return Result.Failure<PermissionResponse>(new Error("Permission", $"Permission {request.PermissionId} Not Found"));
                }
                
                return new PermissionResponse
                {
                    Id = permission.Id.ToString(),
                    KeyId = permission.KeyId.ToString(),
                    EmployeeLastName = permission.EmployeeLastName,
                    EmployeeName = permission.EmployeeName,
                    PermissionDate = permission.PermissionDate,
                    PermissionTypeId = permission.PermissionType.KeyId.ToString(),
                    PermissionType = permission.PermissionType.Description
                };

            }
            catch (Exception ex)
            {
                return Result.Failure<PermissionResponse>(new Error("Exception", ex.Message));
            }
        }

    }


}
