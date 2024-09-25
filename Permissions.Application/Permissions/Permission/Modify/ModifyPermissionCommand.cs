using Permissions.Application.Abstractions.Messaging;

namespace Permissions.Application.Permissions.Permission.Request
{
    public sealed record ModifyPermissionCommand
    (
        string PermissionId,
        string EmployeeName,
        string EmployeeLastName,
        string PermissionTypeId,
        DateOnly PermissionDate
    ) : ICommand<Guid>;
}
