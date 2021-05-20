using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    /// <summary>
    /// Camstar object reference
    /// <para>
    /// This is just a reference to camstar object
    /// and may miss many fields of a real camstar object
    /// </para>
    /// </summary>
    public abstract class CamstarObject
    {
        private string _instanceType;

        internal XElement XmlObject { get; }

        /// <summary>
        /// Camstar object type, like Spec, Container, Workflow etc.
        /// </summary>
        public string InstanceType
        {
            get => _instanceType;
            set
            {
                _instanceType = value;
                XmlObject.Name = value;
            }
        }

        /// <summary>
        /// Unique name of camstar object instance
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Unique id of camstar object instance
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// Indicate whether this object is referenced by id
        /// </summary>
        public bool IsById { get; } = false;

        /// <summary>
        /// Child objects added to this camstar object
        /// </summary>
        public List<CamstarObject> ChildObjects { get; } = new List<CamstarObject>();

        /// <summary>
        /// Construct a camstar object by type name without instance name or id
        /// </summary>
        /// <param name="instanceType">Camstar instance type</param>
        public CamstarObject(string instanceType)
        {
            if (string.IsNullOrEmpty(instanceType))
            {
                throw new ArgumentNullException(nameof(instanceType));
            }
            _instanceType = instanceType;
            XmlObject = new XElement(instanceType);
        }

        /// <summary>
        /// Construct a camstar object by type name and instance name or id
        /// </summary>
        /// <param name="instanceType">Camstar instance type</param>
        /// <param name="instanceNameOrId">instance name or id</param>
        /// <param name="isById">whether this instance is identified by name or id</param>
        public CamstarObject(string instanceType, string instanceNameOrId, bool isById = false)
            : this(instanceType)
        {
            if (instanceNameOrId == null)
            {
                throw new ArgumentNullException(nameof(instanceNameOrId));
            }

            IsById = isById;
            if (IsById)
            {
                Id = instanceNameOrId;
            }
            else
            {
                Name = instanceNameOrId;
            }

            string tagName = isById ? "__Id" : "__name";
            XmlObject.Add(new XElement(tagName, new XCData(instanceNameOrId)));
        }

        /// <summary>
        /// Add camstar object as child of current object
        /// </summary>
        /// <param name="obj">Child camstar object instance</param>
        /// <returns>Current camstar object instance</returns>
        public virtual CamstarObject Add(CamstarObject obj)
        {
            ChildObjects.Add(obj);
            XmlObject.Add(obj.XmlObject);
            return this;
        }

        public override string ToString() => XmlObject.ToString();
    }
}
