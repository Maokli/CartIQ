using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CartIQ.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> _logger;
    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request Received; {Name} {@Request}", typeof(TRequest).Name, request);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        
        try
        {
            var response = await next();
            _logger.LogInformation("Request Succeeded in {Elapsed}s; {Name} {@Request}", stopwatch.Elapsed.TotalSeconds, typeof(TRequest).Name, request);
            return response;
        }
        catch (Exception e)
        {
            _logger.LogInformation("Request Failed in {Elapsed}s; {Name} {@Request}", stopwatch.Elapsed.TotalSeconds, typeof(TRequest).Name, request);
            throw;
        }
    }
}