using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class CamstarClient
    {
        public const char EndSymbol = '\0';

        private static readonly ActivitySource s_activitySource = new("camstar");
        private readonly string _hostName;
        private readonly int _port;

        public int TimeoutInSeconds { get; set; } = 300;

        public int MaxRetries { get; set; } = 3;

        public CamstarClient(string hostName, int port)
        {
            _hostName = hostName;
            _port = port;
        }

        public async Task<ResponseData> SendAsync(DocumentObject doc)
        {
            using var activity = s_activitySource.StartActivity(string.Join(",", doc.Services.Select(s => s.InstanceType)), ActivityKind.Client);

            if (activity?.IsAllDataRequested == true)
            {
                activity.SetTag("camstar.host", _hostName);
                activity.SetTag("camstar.port", _port);
            }

            bool shouldRetry = false;
            int retried = 0;
            ResponseData response;

            do
            {
                try
                {
                    response = await SendRequestAsync(doc);

                    if (activity?.IsAllDataRequested == true)
                    {
                        activity.SetTag("otel.status_code", response.HasError ? "ERROR" : "OK");
                        if (response.HasError)
                        {
                            activity.SetTag("otel.status_description", response.Error);
                        }
                    }

                    if (response.Error?.Contains("redo") == true)
                    {
                        shouldRetry = true;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is SocketException)
                    {
                        shouldRetry = true;
                    }

                    if (activity?.IsAllDataRequested == true)
                    {
                        activity.SetTag("otel.status_code", "ERROR");
                        activity.SetTag("otel.status_description", ex.Message);
                        activity.SetTag("request_xml", doc.ToString());
                    }

                    response = ResponseData.FromError(ex.Message);
                }
                finally
                {
                    retried++;
                }
            } while (shouldRetry && retried < MaxRetries);

            return response;
        }

        private async Task<ResponseData> SendRequestAsync(DocumentObject doc)
        {
            using var client = new TcpClient
            {
                SendTimeout = TimeoutInSeconds * 1000,
                ReceiveTimeout = TimeoutInSeconds * 1000
            };
            await client.ConnectAsync(_hostName, _port);
            using var stream = client.GetStream();
            using var writer = new StreamWriter(stream, Encoding.Unicode)
            {
                AutoFlush = false
            };
            using var reader = new StreamReader(stream, Encoding.Unicode);
            string requestXml = doc.ToString() + EndSymbol;
            await writer.WriteAsync(requestXml);
            await writer.FlushAsync();
            string responseText = await reader.ReadToEndAsync();
            return new ResponseData(responseText);
        }
    }
}
