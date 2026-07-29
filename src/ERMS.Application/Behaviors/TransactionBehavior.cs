using ERMS.Application.Abstractions.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Behaviors
{
    public sealed class TransactionBehavior<TRequest, TResponse>
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
