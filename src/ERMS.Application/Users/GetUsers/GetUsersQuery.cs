using ERMS.SharedKernel.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Users.GetUsers
{
    public sealed record GetUsersQuery(int Page = 1, int PageSize = 10): IRequest<Result<GetUsersResponse>>;
}
