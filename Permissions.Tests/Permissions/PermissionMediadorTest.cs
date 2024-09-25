using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Permissions.API.Controllers.V1.Permissions.Permission;
using Permissions.API.Controllers.V1.Permissions.PermissionType;
using Permissions.Application.Permissions.Permission.Request;
using Permissions.Application.Permissions.PermissionType.Insert;
using Permissions.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Tests.Permissions
{
    public class PermissionMediadorTest
    {
        readonly Mock<ISender> _mockSender;
        readonly PermissionController _permissionController;
        readonly PermissionTypeController _permissionTypeController;

        public PermissionMediadorTest()
        {
            _mockSender = new Mock<ISender>();
            _permissionController = new PermissionController(_mockSender.Object);
            _permissionTypeController = new PermissionTypeController(_mockSender.Object);
        }

        /*
        [Fact]
        public async Task PermissionRequest_ShouldReturnCreatedResult()
        {
            Guid guid = new Guid();

            var AddPermissionTypeCommand = new PermissionTypeInsertCommand(
                  "TEST" 
                );

            _mockSender.Setup(m => m.Send(It.IsAny<PermissionTypeInsertCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Guid>.Success(guid));



            var RequestPermissionCommand = new RequestPermissionCommand(
             "JOSE",
             "MATA",
             Guid.NewGuid().ToString(),
             DateOnly.FromDateTime(DateTime.Today)
             );

            _mockSender.Setup(m => m.Send(It.IsAny<PermissionTypeInsertCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(Result<Guid>.Success(guid));

        }*/
    }
}
