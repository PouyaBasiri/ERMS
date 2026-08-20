using System;
using System.Collections.Generic;
using System.Net.ServerSentEvents;
using System.Text;

namespace ERMS.Application.Users.GetUsers
{
    public sealed record GetUsersResponse(IReadOnlyList<UserItem> Items);
    public sealed record UserItem(Guid Id,string FullName,string Email);
}
