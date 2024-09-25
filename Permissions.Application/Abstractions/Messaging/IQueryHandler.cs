using Permissions.Domain.Abstractions;
using MediatR;
using Permissions.Domain.Abstractions.Messaging;

namespace Permissions.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}