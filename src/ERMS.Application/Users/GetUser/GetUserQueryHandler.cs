using ERMS.Domain.Users;
using ERMS.SharedKernel.Results;
using MediatR;


namespace ERMS.Application.Users.GetUser
{
    public sealed class GetUserQueryHandler: IRequestHandler<GetUserQuery, Result<GetUserResponse>>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetUserQueryHandler(
            IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<Result<GetUserResponse>> Handle(GetUserQuery request,CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetByIdAsync(request.Id,cancellationToken);

            if (user is null)
            {
                return Result<GetUserResponse>.Failure(UserErrors.NotFound);
            }

            return Result<GetUserResponse>.Success(
                new GetUserResponse(
                    user.Id,
                    user.FullName.ToString(),
                    user.Email.Value));
        }
    }
}
