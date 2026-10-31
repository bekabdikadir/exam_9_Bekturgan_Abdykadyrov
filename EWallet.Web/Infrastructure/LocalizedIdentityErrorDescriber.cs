using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Infrastructure;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public LocalizedIdentityErrorDescriber(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
    }

    public override IdentityError DefaultError()
    {
        return new IdentityError { Code = nameof(DefaultError), Description = _localizer["Identity_Default"] };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError { Code = nameof(PasswordTooShort), Description = _localizer["Identity_PasswordTooShort", length] };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = _localizer["Identity_PasswordRequiresDigit"] };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError { Code = nameof(PasswordRequiresLower), Description = _localizer["Identity_PasswordRequiresLower"] };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = _localizer["Identity_PasswordRequiresUpper"] };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = _localizer["Identity_PasswordRequiresNonAlphanumeric"] };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError { Code = nameof(DuplicateUserName), Description = _localizer["Identity_DuplicateUserName"] };
    }

    public override IdentityError InvalidUserName(string? userName)
    {
        return new IdentityError { Code = nameof(InvalidUserName), Description = _localizer["Identity_InvalidUserName"] };
    }

    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError { Code = nameof(DuplicateEmail), Description = _localizer["Identity_DuplicateEmail"] };
    }

    public override IdentityError InvalidEmail(string? email)
    {
        return new IdentityError { Code = nameof(InvalidEmail), Description = _localizer["Identity_InvalidEmail"] };
    }
}
