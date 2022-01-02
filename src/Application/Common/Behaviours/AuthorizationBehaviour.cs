using MediatR;
using MiniRent.Application.Common.Interfaces;

namespace MiniRent.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehaviour(
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            //if (authorizeAttributes.Any())
            //{
            //    // Must be authenticated user
            //    if (_currentUserService.UserId == null)
            //    {
            //        throw new UnauthorizedAccessException();
            //    }
            //}

            var temp = _currentUserService.UserId;

            // User is authorized / authorization not required
            return await next();
        }
    
}
