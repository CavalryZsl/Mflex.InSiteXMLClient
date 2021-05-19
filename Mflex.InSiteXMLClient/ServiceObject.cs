using System;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class ServiceObject : CamstarObject
    {
        public InputData InputData { get; }

        public RequestData RequestData { get; }

        public ServiceObject(string serviceType) : base("__service")
        {
            XmlObject.SetAttributeValue("__serviceType", serviceType);
            XmlObject.Add(new XElement("__txnGUID", Guid.NewGuid().ToString()));
            XmlObject.Add(new XElement("__utcOffset", "+08:00"));
            InputData = AddInputData();
            XmlObject.Add(new ExecuteObject().XmlObject);
            RequestData = AddRequestData();
        }

        private InputData AddInputData()
        {
            var inputData = new InputData();
            XmlObject.Add(inputData.XmlObject);
            return inputData;
        }

        private RequestData AddRequestData()
        {
            var requestData = new RequestData();
            XmlObject.Add(requestData.XmlObject);
            return requestData;
        }
    }
}
