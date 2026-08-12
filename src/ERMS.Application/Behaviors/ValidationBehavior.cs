using ERMS.SharedKernel.Results;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull 
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var failures = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var errors = failures
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .ToList();

            if (errors.Count != 0)
            {
                var message = string.Join(
                    Environment.NewLine,
                    errors.Select(x => x.ErrorMessage));

                return (TResponse)(object)Result<TResponse>.Failure(
                     SharedKernel.Errors.Error.Validation(
                        "Validation.Failed",
                        message));
            }

            return await next();
        }
    }
}
