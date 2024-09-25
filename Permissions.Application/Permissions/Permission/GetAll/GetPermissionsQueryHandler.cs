using Permissions.Application.Abstractions.Messaging;
using Permissions.Application.DTO;
using Permissions.Application.Permissions.Permissions;

using Permissions.Domain.Abstractions;
using Permissions.Domain.Repositories.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.PermissionType.GetAll
{

    internal sealed class GetPermissionsQueryHandler : IQueryHandler<GetPermissionsQuery, DataTableResponse<PermissionResponse>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionsQueryHandler(
        IPermissionRepository permissionRepository,
        IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DataTableResponse<PermissionResponse>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var list = await _permissionRepository.GetPaginateAsync(request.page, request.perPage, cancellationToken);

                var data =  list!.Select(x => new PermissionResponse
                {
                    Id = x.Id.ToString(),
                    KeyId = x.KeyId.ToString(),
                    EmployeeName = x.EmployeeName,
                    EmployeeLastName = x.EmployeeLastName,
                    PermissionType = x.PermissionType.Description,
                    PermissionDate = x.PermissionDate
                }).ToList();


                var count = await _permissionRepository.GetCountAsync(cancellationToken);

                return new DataTableResponse<PermissionResponse> { List = data, Total = count };

            }
            catch (Exception ex)
            {
                return Result.Failure<DataTableResponse<PermissionResponse>>(new Error("Exception", ex.Message));
            }
        }

    }


}

