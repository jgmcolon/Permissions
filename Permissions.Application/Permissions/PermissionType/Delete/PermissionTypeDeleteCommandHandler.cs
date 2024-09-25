using Permissions.Application.Abstractions.Messaging;
using Permissions.Application.Extensions;
using Permissions.Application.Permissions.PermissionType.Insert;
using Permissions.Domain.Abstractions;
using Permissions.Domain.Repositories.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.PermissionType.Delete
{

    internal sealed class PermissionTypeDeleteCommandHandler : ICommandHandler<PermissionTypeDeleteCommand>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionTypeDeleteCommandHandler(
        IPermissionTypeRepository permissionTypeRepository,
        IUnitOfWork unitOfWork)
        {
            _permissionTypeRepository = permissionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(PermissionTypeDeleteCommand request, CancellationToken cancellationToken)
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

                _permissionTypeRepository.Delete(permissionType);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("Exception", ex.Message));
            }
        }

    }
}
