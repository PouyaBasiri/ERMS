using ERMS.SharedKernel.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Users.GetUser
{
    public sealed record GetUserQuery(Guid Id): IRequest<Result<GetUserResponse>>;
}
