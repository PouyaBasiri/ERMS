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

        public async Task<Result<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var page = request.Page < 1 ? 1 : request.Page;
            var pageSize = request.Page is < 1 or > 100 ? 10 : request.PageSize;

            var result = await _userReadRepository.GetPagedAsync(page, pageSize, cancellationToken);


            var items = result.Users
                .Select(user => new UserItem(
                    user.Id,
                    user.FullName.ToString(),
                    user.Email.Value))
                .ToList();

            var totalPages = result.TotalCount == 0
                  ? 0
                  : (int)Math.Ceiling((double)result.TotalCount / pageSize);


            return Result<GetUsersResponse>.Success(new GetUsersResponse(items, page, pageSize, result.TotalCount, totalPages));
        }
    }
}
