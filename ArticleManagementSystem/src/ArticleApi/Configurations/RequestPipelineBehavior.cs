using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ArticleApi.Configurations
{
    public class RequestPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class where TResponse : class
    {
        private readonly ILogger<RequestPipelineBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestPipelineBehavior(ILogger<RequestPipelineBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger;
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);


            _logger.LogInformation("Request Handling : {RequestType}", nameof(TRequest));

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            var serializedRequest = JsonSerializer.Serialize(request);
            var serializedResponse = JsonSerializer.Serialize(response);
            timer.Stop();

            using (_logger.BeginScope("Request Model : {RequestModel}", serializedRequest))
            {
                _logger.LogInformation("Request Model : {RequestModel}", serializedRequest);
                _logger.LogInformation("Response Model : {ResponseModel}", serializedResponse);
                _logger.LogInformation("Execution Time : {ExecutionTime}", timer.ElapsedMilliseconds);
            }

            return response;
        }
    }
}