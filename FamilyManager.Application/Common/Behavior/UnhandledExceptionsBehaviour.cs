using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyManager.Application.Common.Behavior
{
    public class UnhandledExceptionsBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<IRequest> _logger;

        public UnhandledExceptionsBehaviour(ILogger<IRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, $"Unhandled error occured for request {requestName}. {request} handler is not found.");
                throw; //Лучше не выкидывать "ничего", укажи конкретный эксепшен
            }
        }
    }
}
