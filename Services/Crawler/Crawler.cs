using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace AllTheTalk.Crawl
{

	public partial class Crawler : ServiceBase
	{
		public Crawler()
		{
			InitializeComponent();
			this.ServiceName = "AllTheTalkCrawler";
			this.CanPauseAndContinue = true;
			this.CanShutdown = true;
			this.CanStop = true;
			this.CanHandleSessionChangeEvent = false;
			this.CanHandlePowerEvent = false;
		}

		protected override void OnStart(string[] args)
		{
			// TODO: Start the threads
		}


		protected override void OnPause()
		{

			// TODO: Pause the threads.

		}

		protected override void OnContinue()
		{

			// TODO: Restart the threads.

		}

		protected override void OnStop()
		{

			this.RequestAdditionalTime(5000);// TODO: Monitor the shutdown procedure - request add'l time if needed.
			Config.ShuttingDown = true;

			// TODO: Shutdown all the threads.

			this.ExitCode = 0;

		}
	}
}
