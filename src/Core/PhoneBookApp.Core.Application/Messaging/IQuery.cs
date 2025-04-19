using MediatR;
using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Core.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
