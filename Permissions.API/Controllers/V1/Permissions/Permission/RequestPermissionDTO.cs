namespace Permissions.API.Controllers.V1.Permissions.Permission
{
    public sealed record RequestPermissionDTO(
        string EmployeeName,
        string EmployeeLastName,
        string PermissionTypeId,
        DateOnly PermissionDate
    );
}
