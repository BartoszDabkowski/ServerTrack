using ServerTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ServerTracker.Persistence
{
    public class InMemoryContext
    {
        public LoadRecordsByServer LoadRecordsByServer { get; set; }

        public InMemoryContext()
        {
            LoadRecordsByServer = new LoadRecordsByServer();
        }

    }
}