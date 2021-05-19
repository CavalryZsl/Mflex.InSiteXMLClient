using System;

namespace Mflex.InSiteXMLClient
{
    /// <summary>
    /// Camstar object used to indicate <seealso cref="ContainerObject">Container</seealso> level
    /// </summary>
    public class ContainerLevelObject : NamedDataObject
    {
        public ContainerLevelObject(string levelNameOrId, bool isById = false)
            : base("__level", levelNameOrId, isById)
        {

        }
    }
}