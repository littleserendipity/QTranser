using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace QTestsWindow
{
    public class SampleMessageHandler : HttpClientHandler
    {
        private string _displayMessage;
        public SampleMessageHandler(string message)
        {
            _displayMessage = message;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            WriteLine($"In SampleMessageHandler {_displayMessage}");
            if (_displayMessage == "error")
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return Task.FromResult<HttpResponseMessage>(response);
            }

            return base.SendAsync(request, cancellationToken);
        }

    }
}
