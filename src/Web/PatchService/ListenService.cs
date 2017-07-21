using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server;

namespace PatchService
{
    public partial class ListenService : ServiceBase
    {
        public ListenService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread thread = new Thread(
                p =>
                    {
                        try
                        {
                            EasyServer server = new EasyServer();
                            server.ListenTcp();
                        }
                        catch (Exception ex)
                        {
                            EventLog.WriteEntry(ex.Message);
                            throw;
                        }
                    });
            thread.IsBackground = true;
            thread.Start();
        }

        protected override void OnStop()
        {
        }
    }
}
