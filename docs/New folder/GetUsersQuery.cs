using ERMS.SharedKernel.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Users.GetUsers
{
    public sealed record GetUsersQuery: IRequest<Result<GetUsersResponse>>;
}
