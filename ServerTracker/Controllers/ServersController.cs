using ServerTracker.Dtos;
using ServerTracker.Models;
using ServerTracker.Persistence;
using System;
using System.Web.Http;

namespace ServerTracker.Controllers
{
    public class ServersController : ApiController
    {
        private readonly LoadRecordsByServer _loadRecordsByServer;

        public ServersController()
        {
            var inMemoryContext = (InMemoryContext)System.Web.HttpRuntime.Cache.Get("InMemoryContext");
            _loadRecordsByServer = inMemoryContext.LoadRecordsByServer;
        }

        [HttpGet]
        public IHttpActionResult GetRamLoads(string serverName)
        {
            if (!_loadRecordsByServer.ContainsServer(serverName))
                return NotFound();

            var ramLoadAverages = _loadRecordsByServer.GetRamLoadAverages(serverName);

            return Ok(ramLoadAverages);
        }

        [HttpPost]
        public IHttpActionResult RecordLoads(CreateServerLoadDto serverLoadDto)
        {
            if (!_loadRecordsByServer.ContainsServer(serverLoadDto.ServerName))
                return NotFound();

            _loadRecordsByServer.AddLoadForServer(serverLoadDto.ServerName, 
                new LoadRecord()
                {
                    DateTime = DateTime.Now,
                    CpuLoad = serverLoadDto.CpuLoad,
                    RamLoad = serverLoadDto.RamLoad
                });

            return Ok(serverLoadDto);
        }


        // --------------------------------------------------------------------------------------------
        // For Testing Purposes Only
        public ServersController(bool isRandom)
        {
            //Create Server and populate LoadRecords in memory
            System.Web.HttpRuntime.Cache["InMemoryContext"] = new InMemoryContext();
            var inMemoryContext = (InMemoryContext)System.Web.HttpRuntime.Cache["InMemoryContext"];
            _loadRecordsByServer = inMemoryContext.LoadRecordsByServer;
            _loadRecordsByServer.AddServer(Global.Utility.PopulatedServer);
            _loadRecordsByServer.AddServer(Global.Utility.NewServer);

            if (!isRandom)
                _loadRecordsByServer.InitializeServersLoadRecordsRandom(Global.Utility.PopulatedServer, 0.1, 1.5);
            else
                _loadRecordsByServer.InitializeServersLoadRecords(Global.Utility.PopulatedServer, Global.Utility.LoadRate);
        }
        // --------------------------------------------------------------------------------------------
    }
}
