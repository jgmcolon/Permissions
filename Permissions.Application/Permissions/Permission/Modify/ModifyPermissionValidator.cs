using FluentValidation;

namespace Permissions.Application.Permissions.Permission.Request
{
    public class ModifyPermissionValidator : AbstractValidator<ModifyPermissionCommand>
    {
        public ModifyPermissionValidator()
        {
            //RuleFor(c => c.PermissionDate).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
        }
    }
}
