using Asp.Versioning;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Permissions.Application.Permissions.Permission.Request;
using Permissions.Application.Permissions.PermissionType.Insert;
using System.Threading;
using Permissions.Application.Extensions;
using Permissions.Application.Permissions.PermissionType.Delete;
using Permissions.Application.Permissions.PermissionType.Get;
using Permissions.Application.Permissions.PermissionType.GetAll;

namespace Permissions.API.Controllers.V1.Permissions.PermissionType
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PermissionTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public PermissionTypeController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken, [FromQuery] string? filter = null)
        {

            var result = await _sender.Send(new GetPermissionTypesQuery(filter), cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var command = new GetPermissionTypeQuery(
               id
              );

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PermissionTypeDTO request, CancellationToken cancellationToken)
        {
            var command = new PermissionTypeInsertCommand(
                 request.Description
                );

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] PermissionTypeDTO request, CancellationToken cancellationToken)
        {

            var command = new PermissionTypeUpdateCommand(
                id,
                request.Description
               );

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Get), new { id = result.Value }, result.Value);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new PermissionTypeDeleteCommand(id);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}


