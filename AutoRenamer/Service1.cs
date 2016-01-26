using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;

namespace AutoRenamer
{
    public partial class Service1 : ServiceBase
    {
        private bool run = true;


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