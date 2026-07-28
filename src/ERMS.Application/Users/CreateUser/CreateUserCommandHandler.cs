using ERMS.Domain.Users.Rules;
using ERMS.Application.Abstractions.Messaging;
using ERMS.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Text;
using ERMS.Domain.Users.ValueObjects;

namespace ERMS.Domain.Users.CreateUser
{
    public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse> 
    { 
        private readonly IUserRepository _repository; 
        public CreateUserCommandHandler(IUserRepository repository)
        { 
            _repository = repository;
        } 
        public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken) 
        { 
            var fullNameResult = FullName.Create(request.FirstName, request.LastName); 
            if (fullNameResult.IsFailure) return Result<CreateUserResponse>.Failure(fullNameResult.Error);
            var emailResult = Email.Create(request.Email); 
            if (emailResult.IsFailure)
                return Result<CreateUserResponse>.Failure(emailResult.Error); 
            var exists = await _repository.ExistsByEmailAsync(emailResult.Value.Value, cancellationToken);
            var rule = new UserEmailMustBeUniqueRule(exists);
            if (rule.IsBroken()) 
                return Result<CreateUserResponse>.Failure(rule.Error);
            var userResult = User.Create(fullNameResult.Value, emailResult.Value);
            if (userResult.IsFailure) 
                return Result<CreateUserResponse>.Failure(userResult.Error); 
            await _repository.AddAsync(userResult.Value, cancellationToken); 
            return Result<CreateUserResponse>.Success(new CreateUserResponse(userResult.Value.Id, userResult.Value.FullName.DisplayName, userResult.Value.Email.Value));
        } 
    }
}
