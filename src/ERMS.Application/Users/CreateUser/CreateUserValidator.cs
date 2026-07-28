using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Users.CreateUser
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand> 
    {
        public CreateUserValidator() 
        { 
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(UserConstants.FirstNameMaxLength);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(UserConstants.LastNameMaxLength);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(UserConstants.EmailMaxLength).EmailAddress(); 
        } 
    }
}
