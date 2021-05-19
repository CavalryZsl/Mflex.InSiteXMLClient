using System.Collections.Generic;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class InputData : CamstarObject
    {
        private readonly IDictionary<string, SubentityList> _subentities = new Dictionary<string, SubentityList>();

        public IEnumerable<SubentityList> Subentities => _subentities.Values;

        public InputData() : base("__inputData")
        {

        }

        public SubentityList AddSubentityList(string name)
        {
            if (!_subentities.ContainsKey(name))
            {
                var list = new SubentityList(name);
                XmlObject.Add(list.XmlObject);
                _subentities.Add(name, list);
            }
            return _subentities[name];
        }

        public override InputData Add(CamstarObject obj)
        {
            return (InputData)(base.Add(obj));
        }

        public override InputData Add(string propertyName, CamstarObject obj)
        {
            return (InputData)(base.Add(propertyName, obj));
        }
    }
}
