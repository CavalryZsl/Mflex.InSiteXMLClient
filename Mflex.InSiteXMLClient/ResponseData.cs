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

        public string? Error => GetError();

        public IEnumerable<string> ServiceErrors
            => _element.Descendants("__service").Select(svc => GetServiceError(svc)).Where(m => m is { Length: > 0 });

        public string? Message => _element.Descendants("CompletionMsg").FirstOrDefault()?.Value;

        public bool HasError => Error is { Length: > 0 };

        public string? this[string propertyName]
            => _element.Descendants("__service").FirstOrDefault()?.Descendants("__responseData").FirstOrDefault()?.Descendants(propertyName).FirstOrDefault()?.Value;

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
        }

        private string? GetError()
        {
            var exceptionData = _element.Descendants("__responseData").FirstOrDefault()?.Descendants("__exceptionData").FirstOrDefault();
            if (exceptionData == null)
            {
                return null;
            }
            return exceptionData.Element("__errorDescription")?.Value ?? exceptionData.Element("__errorSystemMessage")?.Value;
        }

        private static string GetServiceError(XElement svcElement)
        {
            return svcElement
                .Element("__responseData")?
                .Element("__exceptionData")?
                .Element("__errorDescription")?
                .Value ?? string.Empty;
        }
    }
}
