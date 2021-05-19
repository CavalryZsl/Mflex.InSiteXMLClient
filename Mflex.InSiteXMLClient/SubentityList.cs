﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class SubentityList : CamstarObject
    {
        private readonly XElement _listItem = new("__listItem", new XAttribute("__listItemAction", "add"));
        private readonly List<CamstarObject> _childObjects = new List<CamstarObject>();

        public new IEnumerable<CamstarObject> ChildObjects => _childObjects;

        public SubentityList(string name) : base(name)
        {
            XmlObject.Add(_listItem);
        }

        public override SubentityList Add(CamstarObject obj)
        {
            _childObjects.Add(obj);
            _listItem.Add(obj.XmlObject);
            return this;
        }

        public override SubentityList Add(string propertyName, CamstarObject obj)
        {
            if (obj.TypeName != propertyName)
            {
                obj.TypeName = propertyName;
            }
            return Add(obj);
        }
    }
}
