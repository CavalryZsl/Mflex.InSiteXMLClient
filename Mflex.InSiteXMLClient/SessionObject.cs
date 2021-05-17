using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class SessionObject : CamstarObject
    {
        public string? UserName { get; }

        public string? SessionId { get; }

        public SessionObject(string userName, string password) : base("__session")
        {
            UserName = userName;
            var userElem = new NamedDataObject("user", userName);
            var passwordElem = new XElement("password", new XAttribute("__encrypted", "no"), new XCData(password));
            var connectElem = new XElement("__connect");
            connectElem.Add(userElem.ElementItem);
            connectElem.Add(passwordElem);
            ElementItem.Add(connectElem);
        }
    }
}
