using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class DocumentObject : CamstarObject
    {
        public IDictionary<string, ServiceObject> Services { get; } = new Dictionary<string, ServiceObject>();

        public SessionObject Session { get; }

        public DocumentObject(string userName, string password)
            : this(new SessionObject(userName, password))
        {

        }

        public DocumentObject(SessionObject session)
            : base("__InSite")
        {
            ElementItem.Add(
                new XAttribute("__version", "1.1"),
                new XAttribute("__encryption", "2"));

            Session = session;
            ElementItem.Add(Session.ElementItem);
        }

        public ServiceObject AddService(string serviceName)
        {
            if (!Services.ContainsKey(serviceName))
            {
                Services.Add(serviceName, new ServiceObject(serviceName));
            }
            var obj = Services[serviceName];
            ElementItem.Add(obj.ElementItem);
            return obj;
        }
    }
}
