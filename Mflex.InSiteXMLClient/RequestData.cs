using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class RequestData : CamstarObject
    {
        public RequestData() : base("__requestData")
        {
            ElementItem.Add(new XElement("CompletionMsg"));
        }

        public RequestData Add(string propertyName)
        {
            ElementItem.Add(new XElement(propertyName));
            return this;
        }
    }
}
