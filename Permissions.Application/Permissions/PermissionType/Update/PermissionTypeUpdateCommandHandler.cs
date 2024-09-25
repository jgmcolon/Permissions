using Permissions.Application.Abstractions.Messaging;
using Permissions.Application.Extensions;
using Permissions.Application.Permissions.Permission.Request;
using Permissions.Domain.Abstractions;
using Permissions.Domain.Repositories.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PermissionTypeEntity = Permissions.Domain.Entities.Permissions.PermissionType;

namespace Permissions.Application.Permissions.PermissionType.Insert
{
    internal sealed class PermissionTypeUpdateCommandHandler : ICommandHandler<PermissionTypeUpdateCommand, Guid>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionTypeUpdateCommandHandler(
        IPermissionTypeRepository permissionTypeRepository,
        IUnitOfWork unitOfWork)
        {
            _permissionTypeRepository = permissionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(PermissionTypeUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (!request.PermissionTypeId.IsGuid())
                {
                    return Result.Failure<Guid>(new Error("permissionType", $"{request.PermissionTypeId} not is GUID"));
                }

                var permissionType = await _permissionTypeRepository.GetByIdAsync(request.PermissionTypeId.ToGuid(), cancellationToken);

                if (permissionType is null)
                {
                    return Result.Failure<Guid>(new Error("permissionType", $"Permission Type {request.PermissionTypeId} Not Found"));
                }

                var row = new PermissionTypeEntity
                {
                    Description = request.Description,
                };

                _permissionTypeRepository.Update(row);

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
