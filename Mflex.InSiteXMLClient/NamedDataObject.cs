using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class NamedDataObject : CamstarObject
    {
        public NamedDataObject(string typeName, string nameOrId, bool isById = false) : base(typeName, nameOrId, isById)
        {

        }
    }
}
