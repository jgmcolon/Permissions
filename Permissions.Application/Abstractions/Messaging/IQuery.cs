using Permissions.Domain.Abstractions;
using MediatR;

namespace Permissions.Domain.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}