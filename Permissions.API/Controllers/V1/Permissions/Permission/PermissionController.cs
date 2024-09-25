using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Permissions.Application.Permissions.Permission.Request;
using Permissions.Application.Permissions.PermissionType.Get;
using Permissions.Application.Permissions.PermissionType.GetAll;

namespace Permissions.API.Controllers.V1.Permissions.Permission
{

    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/permission")]
    public class PermissionController : ControllerBase
    {
        private readonly ISender _sender;

        public PermissionController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken, [FromHeader] int page = 1, [FromHeader] int perPage = 10)
        {

            var result = await _sender.Send(new GetPermissionsQuery("", page, perPage), cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var command = new GetPermissionQuery(id);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }


        [HttpPost("request")]
        public async Task<IActionResult> Post(RequestPermissionDTO request, CancellationToken cancellationToken)
        {
            var command = new RequestPermissionCommand(
                request.EmployeeName,
                request.EmployeeLastName,
                request.PermissionTypeId,
                request.PermissionDate);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
        }


        [HttpPut("modify/{id}")]
        public async Task<IActionResult> Put(string id, RequestPermissionDTO request, CancellationToken cancellationToken)
        {
            var command = new ModifyPermissionCommand(
                id,
                request.EmployeeName,
                request.EmployeeLastName,
                request.PermissionTypeId,
                request.PermissionDate);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
        }

    }


}
