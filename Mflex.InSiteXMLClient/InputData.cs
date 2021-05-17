using System.Xml.Linq;

namespace Mflex.InSiteXMLClient
{
    public class InputData : CamstarObject
    {
        public InputData() : base("__inputData")
        {

        }

        public SubentityList AddSubentityList(string name)
        {
            var list = new SubentityList(name);
            ElementItem.Add(list.ElementItem);
            return list;
        }
    }
}
