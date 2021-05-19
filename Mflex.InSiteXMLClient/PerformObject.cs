using System;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class PerformObject : CamstarObject
    {
        public string EventName { get; }

        public PerformObject(string eventName) : base("__perform")
        {
            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException(nameof(eventName));
            }
            EventName = eventName;
            XmlObject.Add(new XElement("__eventName", new XCData(eventName)));
        }
    }
}