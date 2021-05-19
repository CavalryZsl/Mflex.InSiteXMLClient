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
        private readonly IDictionary<string, ServiceObject> _services = new Dictionary<string, ServiceObject>();
        public IEnumerable<ServiceObject> Services => _services.Values;
        public SessionObject Session { get; }

        public DocumentObject(string userName, string password)
            : this(new SessionObject(userName, password))
        {

        }

        public DocumentObject(SessionObject session)
            : base("__InSite")
        {
            XmlObject.Add(
                new XAttribute("__version", "1.1"),
                new XAttribute("__encryption", "2"));

            Session = session;
            Add(session);
        }

        public ServiceObject AddService(string serviceName)
        {
            if (!_services.ContainsKey(serviceName))
            {
                var svc = new ServiceObject(serviceName);
                _services.Add(serviceName, svc);
                Add(svc);
            }
            return _services[serviceName];
        }
    }
}
