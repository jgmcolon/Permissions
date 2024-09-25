using Permissions.Application.Abstractions.Messaging;
using Permissions.Application.Extensions;
using Permissions.Application.Permissions.Permissions;
using Permissions.Domain.Abstractions;
using Permissions.Domain.Repositories.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PermissionEntity = Permissions.Domain.Entities.Permissions.Permission;
using PermissionTypeEntity = Permissions.Domain.Entities.Permissions.PermissionType;



namespace Permissions.Application.Permissions.Permission.Request
{
    internal sealed class ModifyPermissionCommandHandler : ICommandHandler<ModifyPermissionCommand, Guid>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModifyPermissionCommandHandler(IPermissionRepository permissionRepository,
            IPermissionTypeRepository permissionTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _permissionTypeRepository = permissionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                if (!request.PermissionId.IsGuid())
                {
                    return Result.Failure<Guid>(new Error("Permission", $"{request.PermissionId} not is GUID"));
                }



                var row = await _permissionRepository.GetByIdAsync(request.PermissionId.ToGuid(), cancellationToken);

                if (row is null)
                {
                    return Result.Failure<Guid>(new Error("Permission", $"Permission {request.PermissionId} Not Found"));
                }

                var permissionType = await _permissionTypeRepository.GetByIdAsync(request.PermissionTypeId.ToGuid(), cancellationToken);

                if (permissionType is null)
                {
                    return Result.Failure<Guid>(new Error("permissionType", $"Permission Type {request.PermissionTypeId} Not Found"));
                }

                row.PermissionDate = request.PermissionDate;
                row.EmployeeLastName = request.EmployeeLastName;
                row.EmployeeName = request.EmployeeName;
                row.PermissionTypeId = permissionType!.Id;

                row.RaiseDomainEvent(new PermissionRegistedDomainEvent(
                               row.KeyId.ToString(),
                               row.EmployeeName,
                               row.EmployeeLastName,
                               row.PermissionTypeId.ToString(),
                               permissionType.Description,
                               row.PermissionDate
                             ));

                _permissionRepository.Update(row);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return row.KeyId;
            }
            catch (Exception ex)
            {
                return Result.Failure<Guid>(new Error("Exception", ex.Message));
            }
        }
    }
}
