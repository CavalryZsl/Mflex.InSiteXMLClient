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
            for (int i = 0; i < 3; i++)
            {
                stopwatch.Restart();
                var client = new CamstarClient("mfcmessmt", 2881);

                var doc = new DocumentObject("jw33002401", "Wj--198312");

                var svc = doc.AddService("M_QuickPass");
                svc.InputData
                    .AddNamedDataObject("Employee", "mes_smt")
                    .AddNamedDataObject("Resource", "HZ-SMT-ET-001")
                    .AddRevisionedDataObject("Spec", "4183ALSB3EP");
                svc.InputData.AddSubentityList("ServiceDetails")
                   .Add(new ContainerObject("C470305022FMDFQ87-AJ75", string.Empty));

                //var svc = doc.AddService("MoveNonStd");
                //svc.InputData
                //    .AddContainer("Container", "C470305022FMDFQ87-AJ75")
                //    .AddRevisionedDataObject("ToWorkFlow", "CAA11067_040-CF008_A_4183")
                //    .AddNamedDataObject("ToStep", "4183MICTB1W");

                Debug.WriteLine(doc);
                var res = client.SendAsync(doc).Result;
                Debug.WriteLine(res.Error ?? res.Message);
                stopwatch.Stop();
                Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            Assert.True(true);
        }
    }
}
