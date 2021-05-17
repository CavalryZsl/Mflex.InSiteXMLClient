using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class SubentityList : CamstarObject
    {
        private readonly XElement _listItem = new("__listItem", new XAttribute("__listItemAction", "add"));

        public List<CamstarObject> CamstarObjects { get; } = new List<CamstarObject>();

        public SubentityList(string name) : base(name)
        {
            ElementItem.Add(_listItem);
        }

        public override SubentityList Add(CamstarObject obj)
        {
            CamstarObjects.Add(obj);
            _listItem.Add(obj.ElementItem);
            return this;
        }

        public override SubentityList Add(string propertyName, CamstarObject obj)
        {
            obj.TypeName = propertyName;
            return Add(obj);
        }
    }
}
