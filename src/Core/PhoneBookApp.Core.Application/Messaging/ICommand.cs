using MediatR;
using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Core.Application.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
