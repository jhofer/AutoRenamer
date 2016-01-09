using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoRenamer
{
    public partial class Service1 : ServiceBase
    {
        private bool run;


        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
                Thread tr = new Thread(Execute);
               tr.Start();
        }

        private void Execute()
        {
            int sleep = Int32.Parse(ConfigurationSettings.AppSettings["sleep"]);
            Renamer renamer = new Renamer();
            while (run)
            {
                renamer.Run();
               Thread.Sleep(sleep);
            }
        }


        protected override void OnStop()
        {
            run = false;
        }
    }
}
