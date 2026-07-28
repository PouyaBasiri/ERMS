using ERMS.SharedKernel.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Abstractions.Messaging
{
    public interface ICommand<TResponse> : IRequest<Result<TResponse>> 
    { 
    }
}
