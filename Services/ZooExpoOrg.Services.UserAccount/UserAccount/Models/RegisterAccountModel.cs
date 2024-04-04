using FluentValidation;

namespace ZooExpoOrg.Services.UserAccount;

public class RegisterAccountModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterUserAccountModelValidator : AbstractValidator<RegisterAccountModel>
{
    public RegisterUserAccountModelValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name is required.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(50).WithMessage("Password is long.");
    }
}