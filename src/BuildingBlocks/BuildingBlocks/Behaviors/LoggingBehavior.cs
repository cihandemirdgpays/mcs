using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - response={Response}  requestData={RequestData}"
            , typeof(TRequest).Name, typeof(TResponse).Name, request);
        var timer = Stopwatch.StartNew();
        var response = await next();
        timer.Stop();
        var elapsed = timer.Elapsed;
        if (elapsed > TimeSpan.FromSeconds(3))
        {
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}", typeof(TRequest).Name, elapsed);
        }
        logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}