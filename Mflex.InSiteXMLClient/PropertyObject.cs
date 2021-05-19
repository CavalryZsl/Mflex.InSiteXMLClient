using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class PropertyObject : CamstarObject
    {
        public PropertyObject(string propertyName, string value)
            : base(propertyName, value)
        {
            XmlObject.RemoveNodes();
            XmlObject.Add(new XCData(value));
        }
    }
}
