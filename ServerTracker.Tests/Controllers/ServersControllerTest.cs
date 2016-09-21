using System.ComponentModel.Design.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerTracker.Controllers;
using ServerTracker.Dtos;
using System.Web.Http.Results;

namespace ServerTracker.Tests.Controllers
{
    [TestClass]
    public class ServersControllerTest
    {
        //RamLoadAverages will all be 1.5
        [TestMethod]
        public void GetRamLoads_ShouldReturnRamLoads()
        {
            ServersController controller = new ServersController(true);

            var actionResult = controller.GetRamLoads(Global.Utility.PopulatedServer);
            var contentResult = actionResult as OkNegotiatedContentResult<RamLoadAveragesDto>;
            var generatedLoadAverages = Utility.TestHelper.CreateRamLoadAverages(Global.Utility.LoadRate);

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(true, Utility.TestHelper.AreRamLoadAveragesEqual(contentResult.Content, generatedLoadAverages));
        }

        [TestMethod]
        public void GetRamLoads_ShouldReturnEmpty()
        {
            ServersController controller = new ServersController(true);
            var actionResult = controller.GetRamLoads(Global.Utility.NewServer);
            var contentResult = actionResult as OkNegotiatedContentResult<RamLoadAveragesDto>;
            var emptyLoad  = new RamLoadAveragesDto()
                {
                    AverageRamLoadsForLastHourByMinute = new double[0],
                    AverageRamLoadsForLastDayByHour = new double[0]
                };

            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(true, Utility.TestHelper.AreRamLoadAveragesEqual(contentResult.Content, emptyLoad));

        }

        [TestMethod]
        public void GetRamLoads_ShouldReturnNotFound()
        {
            ServersController controller = new ServersController(true);
            var actionResult = controller.GetRamLoads("NotAvailableServer");

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void RecordLoads_ShouldReturnOK()
        {
            ServersController controller = new ServersController(true);
            var actionResult = controller.RecordLoads(new CreateServerLoadDto()
            {
                ServerName = Global.Utility.NewServer,
                CpuLoad = 1.1,
                RamLoad = 1.1
            });

            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<CreateServerLoadDto>));
        }

        [TestMethod]
        public void RecordLoads_ShouldReturnNotFound()
        {
            ServersController controller = new ServersController(true);
            var actionResult = controller.RecordLoads(new CreateServerLoadDto()
            {
                ServerName = "NotAvailableServer",
                CpuLoad = 1.1,
                RamLoad = 1.1
            });

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
