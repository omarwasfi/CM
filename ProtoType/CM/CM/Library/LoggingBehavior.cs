using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Library
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        public LoggingBehavior()
        {

        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType().Name;
            var requestGuid = Guid.NewGuid().ToString();
            var requestNameWithGuid = $"{requestName} [{requestGuid}]";

            Log.Information($"[Start] {requestName} [{requestGuid}]");
            
            TResponse response;
            var stopwatch = Stopwatch.StartNew();

            try
            {
                response = await next();

            }
            finally
            {
                stopwatch.Stop();
                Log.Information($"[END] {requestNameWithGuid};  Execution time={stopwatch.ElapsedMilliseconds}ms");

            }

            return response;
        }
    }
}
