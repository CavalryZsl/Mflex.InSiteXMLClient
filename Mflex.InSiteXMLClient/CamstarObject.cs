using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public abstract class CamstarObject
    {
        private string _typeName;

        internal XElement XmlObject { get; }

        public string TypeName
        {
            get => _typeName;
            set
            {
                _typeName = value;
                XmlObject.Name = value;
            }
        }

        public string? Name { get; }

        public string? Id { get; }

        public bool IsById { get; } = false;

        public List<CamstarObject> ChildObjects { get; } = new List<CamstarObject>();

        public CamstarObject(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException(nameof(typeName));
            }
            _typeName = typeName;
            XmlObject = new XElement(typeName);
        }

        public CamstarObject(string typeName, string nameOrId, bool isById = false)
            : this(typeName)
        {
            if (nameOrId == null)
            {
                throw new ArgumentNullException(nameof(nameOrId));
            }

            IsById = isById;
            if (IsById)
            {
                Id = nameOrId;
            }
            else
            {
                Name = nameOrId;
            }

            string tagName = isById ? "__Id" : "__name";
            XmlObject.Add(new XElement(tagName, new XCData(nameOrId)));
        }

        public virtual CamstarObject Add(CamstarObject obj)
        {
            ChildObjects.Add(obj);
            XmlObject.Add(obj.XmlObject);
            return this;
        }

        public virtual CamstarObject Add(string propertyName, CamstarObject obj)
        {
            if (propertyName != obj.TypeName)
            {
                obj.TypeName = propertyName;
            }
            return Add(obj);
        }

        public override string ToString() => XmlObject.ToString();
    }
}
