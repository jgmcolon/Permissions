using MediatR;
using Permissions.Domain.Abstractions;
using Permissions.Domain.IndexSearch.Permissions;
using Permissions.Domain.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndexPermissionDto = Permissions.Domain.IndexDto.Permissions.PermissionDto;


namespace Permissions.Application.Permissions.Permission.Request
{
    internal sealed class PermissionRegistedDomainEventHandler : INotificationHandler<PermissionRegistedDomainEvent>
    {
        private readonly IPermissionIndex _permissionIndex;
        private readonly IPermissionStreaming _permissionStreaming;
        public PermissionRegistedDomainEventHandler(
                IPermissionIndex permissionIndex,
                IPermissionStreaming permissionStreaming
            )
        {
            _permissionIndex = permissionIndex;
            _permissionStreaming = permissionStreaming;
        }

        public async Task Handle(PermissionRegistedDomainEvent permission, CancellationToken cancellationToken)
        {
            //Notify to ElasticSarch

            var dto = new IndexPermissionDto(
                    permission.PermissionId,
                    permission.EmployeeName,
                    permission.EmployeeLastName,
                    permission.PermissionTypeId,
                    permission.PermissionType,
                    permission.PermissionDate
                );

            await _permissionIndex.Add(dto);

            //Notify to kakla

            await _permissionStreaming.ProduceAsync(dto);

        }


    }
}
