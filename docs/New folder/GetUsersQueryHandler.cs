using ERMS.Domain.Users;
using ERMS.SharedKernel.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Users.GetUsers
{
    public sealed class GetUsersQueryHandler
        : IRequestHandler<GetUsersQuery, Result<GetUsersResponse>>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetUsersQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<Result<GetUsersResponse>> Handle(GetUsersQuery request,CancellationToken cancellationToken)
        {
            var users = await _userReadRepository.GetAllAsync(cancellationToken);

            var items = users
                .Select(user => new UserItem(
                    user.Id,
                    user.FullName.ToString(),
                    user.Email.Value))
                .ToList();

            return Result<GetUsersResponse>.Success(new GetUsersResponse(items));
        }
    }
}
