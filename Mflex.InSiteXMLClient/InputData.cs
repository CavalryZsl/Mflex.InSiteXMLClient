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

        public InputData AddNamedDataObject(string propertyName, string name, bool isById = false)
        {
            return (InputData)Add(new NamedDataObject(propertyName, name, isById));
        }

        public InputData AddRevisionedDataObject(string propertyName, string name, string? revision = null, bool isById = false)
        {
            return (InputData)Add(new RevisionedDataObject(propertyName, name, revision, isById));
        }

        public InputData AddContainer(string propertyName, string name, string level = "", bool isById = false)
        {
            var obj = new ContainerObject(name, level, isById)
            {
                InstanceType = propertyName
            };
            return (InputData)Add(obj);
        }

        public InputData AddProperty(string properName, string value)
        {
            return (InputData)Add(new PropertyObject(properName, value));
        }
    }
}
