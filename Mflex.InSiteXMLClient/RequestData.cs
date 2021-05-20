using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class RequestData : CamstarObject
    {
        public RequestData() : base("__requestData")
        {
            XmlObject.Add(new XElement("CompletionMsg"));
        }

        public RequestData AddProperty(string propertyName)
        {
            XmlObject.Add(new XElement(propertyName));
            return this;
        }
    }
}
