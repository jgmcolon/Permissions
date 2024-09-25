using FluentValidation;

namespace Permissions.Application.Permissions.Permission.Request
{
    public class RequestPermissionValidator : AbstractValidator<RequestPermissionCommand>
    {
        public RequestPermissionValidator()
        {
            //RuleFor(c => c.PermissionDate).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
        }
    }
}
