using System;

namespace Mflex.InSiteXMLClient
{
    public class ContainerLevelObject : NamedDataObject
    {
        public ContainerLevelObject(string levelNameOrId, bool isById = false) : base("__level", levelNameOrId, isById)
        {

        }
    }
}