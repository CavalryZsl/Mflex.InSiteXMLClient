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
            ElementItem.SetAttributeValue("__serviceType", serviceType);
            ElementItem.Add(new XElement("__txnGUID", Guid.NewGuid().ToString()));
            ElementItem.Add(new XElement("__utcOffset", "+08:00"));
            InputData = AddInputData();
            ElementItem.Add(new ExecuteObject().ElementItem);
            RequestData = AddRequestData();
        }

        private InputData AddInputData()
        {
            var inputData = new InputData();
            ElementItem.Add(inputData.ElementItem);
            return inputData;
        }

        private RequestData AddRequestData()
        {
            var requestData = new RequestData();
            ElementItem.Add(requestData.ElementItem);
            return requestData;
        }
    }
}
