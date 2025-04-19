using MediatR;
using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Core.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
