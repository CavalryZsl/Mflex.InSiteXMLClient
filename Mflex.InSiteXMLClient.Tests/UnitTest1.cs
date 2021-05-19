using System;
using System.Diagnostics;
using Xunit;

namespace Mflex.InSiteXMLClient.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var stopwatch = new Stopwatch();
            for (int i = 0; i < 300; i++)
            {
                stopwatch.Restart();
                var client = new CamstarClient("mfcmessmt11", 2882);

                var doc = new DocumentObject("jw3300240111", "Wj--198312");

                //var svc = doc.AddService("M_QuickPass");
                //svc.InputData.Add("Employee", new NamedDataObject("Employee", "mes_smt"));
                //svc.InputData.Add("Resource", new NamedDataObject("Resource", "HZ-SMT-ET-001"));
                //var item = svc.InputData.AddSubentityList("ServiceDetails");
                //item.Add("Container", new ContainerObject("C470305022FMDFQ87-AJ75", string.Empty));
                //svc.InputData.Add("Spec", new RevisionedDataObject("Spec", "4183ALSB3EP"));

                var svc = doc.AddService("MoveNonStd");
                svc.InputData.Add("Container", new ContainerObject("C470305022FMDFQ87-AJ75", string.Empty));
                svc.InputData.Add("ToWorkFlow", new RevisionedDataObject("Workflow", "CAA11067_040-CF008_A_4183"));
                svc.InputData.Add("ToStep", new NamedDataObject("WorkflowStep", "4183MICTB1W"));

                string b = new ContainerObject("C470305022FMDFQ87-AJ75", string.Empty).ToString();
                string a = svc.ToString();
                string req = doc.ToString();
                var res = client.SendAsync(doc).Result;
                Debug.WriteLine(res.Error ?? res.Message);
                stopwatch.Stop();
                Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            Assert.True(true);
        }
    }
}
