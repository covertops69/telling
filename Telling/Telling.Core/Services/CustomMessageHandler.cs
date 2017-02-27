using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telling.Core.Extensions;

namespace Telling.Core.Services
{
    public class CustomMessageHandler : NativeMessageHandler
    {

#if DEBUG
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            HttpResponseMessage response = null;
            var headers = request.Headers;
            var responseString = string.Empty;
            var requestString = string.Empty;
            var outputStringBuilder = new StringBuilder();

            const string LINE_ENDING = "===================================================================================================";
            const string SECTION_ENDING = "---------------------------------------------------------------------------------------------------";

            try
            {
                if (request.Content != null) requestString = await request.Content?.ReadAsStringAsync();
                response = await base.SendAsync(request, cancellationToken);
                responseString = await response.Content?.ReadAsStringAsync();

                outputStringBuilder.AppendLine(LINE_ENDING);

                // Headers
                outputStringBuilder.AppendLine("REQUEST HEADERS:");
                foreach (var header in headers)
                    outputStringBuilder.AppendLine($"HEADER: {header.Key}: {header.Value?.ToList()?.FirstOrDefault()}");
                outputStringBuilder.AppendLine(SECTION_ENDING);

                // Parameters
                outputStringBuilder.AppendLine("REQUEST PARAMS:");
                outputStringBuilder.AppendLine(requestString);
                outputStringBuilder.AppendLine(SECTION_ENDING);

                // Response
                outputStringBuilder.AppendLine("RESPONSE:");
                outputStringBuilder.AppendLine(responseString);
                outputStringBuilder.AppendLine(SECTION_ENDING);

                return response;
            }
            finally
            {
                stopwatch.Stop();
                var totalSize = 0L;

                if (response != null)
                {
                    var bodylength = response.Content.Headers.ContentLength;
                    var headerlength = response.Headers.ToString().Length;
                    totalSize = bodylength.GetValueOrDefault() + headerlength;
                }

                outputStringBuilder.AppendLine(string.Format("REQUEST [{0}:{1}] Time:{2}| Size:{3}| HTTP-CODE:{4}",
                    request.Method.ToString(),
                    request.RequestUri.PathAndQuery,
                    stopwatch.Elapsed.ToString("ss\\.fff"),
                    totalSize.ToPrettyByteSize(),
                    response?.StatusCode.ToString() ?? "No Internet Connectivity"));

                outputStringBuilder.AppendLine(LINE_ENDING);

                Debug.WriteLine("\n" + outputStringBuilder);
            }
        }
#endif
    }
}
