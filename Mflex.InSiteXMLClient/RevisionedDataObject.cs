using System;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class RevisionedDataObject : NamedDataObject
    {
        public string? Revision { get; }

        public bool UseROR => string.IsNullOrEmpty(Revision);

        public RevisionedDataObject(string typeName, string nameOrId, string? revision = null, bool isById = false)
            : base(typeName, nameOrId, isById)
        {
            Revision = revision;
            if (UseROR)
            {
                XmlObject.Add(new XElement("__useROR", "true"));
            }
            else
            {
                XmlObject.Add(new XElement("__rev", new XCData(revision!)));
            }
        }
    }
}
