using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Threading;
using AllTheTalk.Crawl;
using log4net;
using System.Collections.Generic;


public partial class _Default : System.Web.UI.Page
{
	private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	protected void btnGo_Click(object sender, EventArgs e)
	{
		// Pre- Initialize
		Program.PreInitialize();

		// Initialize
		log.Info("Application Started");
		Program.Initialize();

		// Start Workers
		//Thread hostToProcessThread = new Thread(new ThreadStart(Workers.HostToProcessWorker));
		//hostToProcessThread.Start();

		Thread webBrowserThread = new Thread(new ThreadStart(Workers.WebBrowserWorker));
		webBrowserThread.Start();

		Thread remoteUrlQueueThread = new Thread(new ThreadStart(Workers.RemoteUrlQueueWorker));
		remoteUrlQueueThread.Start();

		Thread localUrlQueueThread = new Thread(new ThreadStart(Workers.LocalUrlQueueWorker));
		localUrlQueueThread.Start();

		Thread feedUrlQueueThread = new Thread(new ThreadStart(Workers.FeedUrlQueueWorker));
		feedUrlQueueThread.Start();

		// DEBUG: Wait 60 Seconds, then initialize shutdown.
		Thread.Sleep(60000);
		Program.ShutDown();

		//hostToProcessThread.Join();
		webBrowserThread.Join();
		remoteUrlQueueThread.Join();
		localUrlQueueThread.Join();
		feedUrlQueueThread.Join();

		log.InfoFormat("Exiting Application");

	}

}
