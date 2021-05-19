using System;
using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class ContainerObject : CamstarObject
    {
        public ContainerObject(string nameOrId, ContainerLevelObject level, bool isById = false)
            : base("Container", nameOrId, isById)
        {
            Add(level);
        }

        public ContainerObject(string nameOrId, string levelName, bool isById = false)
            : this(nameOrId, new ContainerLevelObject(levelName), isById)
        {

        }
    }
}