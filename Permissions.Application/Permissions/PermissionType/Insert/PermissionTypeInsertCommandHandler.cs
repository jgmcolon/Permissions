using Permissions.Application.Abstractions.Messaging;
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
    internal sealed class PermissionTypeInsertCommandHandler : ICommandHandler<PermissionTypeInsertCommand, Guid>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionTypeInsertCommandHandler(
        IPermissionTypeRepository permissionTypeRepository,
        IUnitOfWork unitOfWork)
        {
            _permissionTypeRepository = permissionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(PermissionTypeInsertCommand request, CancellationToken cancellationToken)
        {
            try
            {
                

                var row = new PermissionTypeEntity
                {
                    Description = request.Description,
                };

                _permissionTypeRepository.Add(row);

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
