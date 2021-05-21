using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class ResponseData
    {
        private readonly XElement _element;

        public string? Error { get; }
        public string? Message { get; }
        public bool HasError => Error is { Length: > 0 };

        public string? this[string propertyName]
            => _element.Descendants("__service")
            .SelectMany(s => s.Descendants("__responseData"))
            .SelectMany(r => r.Descendants(propertyName))
            .FirstOrDefault()?.Value;

        public static ResponseData FromError(string error)
        {
            return new ResponseData(
                @$"<__InSite __encryption=""2"" __version=""1.1"">
	                    <__service __serviceType=""Error"">
		                    <__responseData>
			                    <__exceptionData>
				                    <__errorDescription>
					                    <![CDATA[{error}]]>
				                    </__errorDescription>
			                    </__exceptionData>
		                    </__responseData>
	                    </__service>
                    </__InSite>");
        }

        public ResponseData(string responseText)
        {
            _element = XElement.Parse(responseText.Trim('\0'));
            var responses = _element.Descendants("__responseData");
            var errors = responses.Select(resp => GetResponseError(resp));
            var messages = responses.Select(resp => GetResponseMessage(resp));

            var svc = _element.Element("__service");
            if (svc != null)
            {
                Error = errors.Where(e => e is { Length: > 0 }).FirstOrDefault();
                Message = messages.Where(m => m is { Length: > 0 }).FirstOrDefault();
            }
        }

        private static string? GetResponseError(XElement? responseData)
        {
            if (responseData == null)
            {
                return null;
            }
            var exData = responseData.Element("__exceptionData");
            if (exData == null)
            {
                return null;
            }
            return exData.Element("__errorDescription")?.Value ?? exData.Element("__errorSystemMessage")?.Value;
        }

        private static string? GetResponseMessage(XElement? responseData) => responseData?.Element("CompletionMsg")?.Value;
    }
}
