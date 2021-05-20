using System;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class SessionObject : CamstarObject
    {
        public string? UserName { get; }

        public string SessionId { get; }

        public SessionObject(string userName, string password) : base("__session")
        {
            SessionId = Guid.NewGuid().ToString();
            UserName = userName;
            XmlObject.Add(new XElement("__connect",
                new XElement("user", new XElement("__name", new XCData(userName))),
                new XElement("password", new XAttribute("__encrypted", "no"), new XCData(password))
                ));
        }
    }
}
