using Permissions.Application.Abstractions.Messaging;
using Permissions.Application.Extensions;
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
    internal sealed class RequestPermissionCommandHandler : ICommandHandler<RequestPermissionCommand, Guid>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestPermissionCommandHandler(IPermissionRepository permissionRepository,
            IPermissionTypeRepository permissionTypeRepository,
            IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _permissionTypeRepository = permissionTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var permissionType = await _permissionTypeRepository.GetByIdAsync(request.PermissionTypeId.ToGuid(), cancellationToken);

                if (permissionType is null)
                {
                    return Result.Failure<Guid>(new Error("permissionType", $"Permission Type {request.PermissionTypeId} Not Found"));
                }


                var row = new PermissionEntity
                {
                    EmployeeName = request.EmployeeName,
                    EmployeeLastName = request.EmployeeLastName,
                    PermissionDate = request.PermissionDate,
                    PermissionTypeId = permissionType!.Id
                };

                _permissionRepository.Add(row);


                row.RaiseDomainEvent(new PermissionRegistedDomainEvent(
                      row.KeyId.ToString(),
                      row.EmployeeName,
                      row.EmployeeLastName,
                      row.PermissionTypeId.ToString(),
                      permissionType.Description,
                      row.PermissionDate
                    ));

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
