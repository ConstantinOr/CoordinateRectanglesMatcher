using CoordinateRectanglesMatcher.Models;
using FluentValidation;

namespace CoordinateRectanglesMatcher.Validators;

public class AuthModelValidator: AbstractValidator<User>
{
    public AuthModelValidator()
    {
        RuleFor(u=>u)
            .Must((u)=>u!=null)
            .WithMessage("The User model should have a value.");
        
        RuleFor(u=>u)
            .Must((u)=>!string.IsNullOrWhiteSpace(u.Password))
            .WithMessage("The Password should have a value.");
        
        RuleFor(u=>u)
            .Must((u)=>!string.IsNullOrWhiteSpace(u.UserName))
            .WithMessage("The UserName should have a value.");
    }
}