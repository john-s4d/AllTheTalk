using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.Data;
using System.Threading;

namespace AllTheTalk.Crawl
{
	public class Workers
	{
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Browses Hosts and sends found urls to the Url Queues
		/// </summary>
		public static void WebBrowserWorker()
		{
			while (!Config.ShuttingDown)
			{
				Uri website = null;

				website = Data.GetHostToProcess();
				WebBrowser browser = new WebBrowser(website);
				browser.Run();
				Data.UpdateHostVisited(website);
			}
		}

		/// <summary>
		/// Moves Remote Urls from the Queue to the Database
		/// </summary>
		public static void RemoteUrlQueueWorker()
		{
			while (!Config.ShuttingDown || (Queues.CountRemoteUrls > 0)) // Empty the Queue on Shutdown
			{
				Uri url = null;

				try
				{
					url = Queues.GetRemoteUrl();
				}
				catch (InvalidOperationException)
				{
					int wait = Config.DefaultWait;
					log.WarnFormat("RemoteUrl Queue is empty. Sleeping {0}", wait.ToString(), Thread.CurrentThread.GetHashCode().ToString());
					Thread.Sleep(wait);
					continue;
				}
				Data.InsertHost(url);

				if (url.PathAndQuery != "/")
					Data.InsertDeepLink(url);
			}
		}

		/// <summary>
		/// Moves Local Urls from the Queue to the Database
		/// </summary>
		public static void LocalUrlQueueWorker()
		{
			// Local Url Queue
			while (!Config.ShuttingDown || (Queues.CountLocalUrls > 0)) // Empty the Queue on Shutdown
			{
				Uri url = null;

				try
				{
					url = Queues.GetLocalUrl();
				}
				catch (InvalidOperationException)
				{
					int wait = Config.DefaultWait;
					log.WarnFormat("LocalUrl Queue is empty. Sleeping {0}", wait.ToString(), Thread.CurrentThread.GetHashCode().ToString());
					Thread.Sleep(wait);
					continue;
				}
				Data.DeleteDeepLink(url);
			}
		}

		/// <summary>
		/// Moves Feed Urls from the Queue to the Database
		/// </summary>
		public static void FeedUrlQueueWorker()
		{
			while (!Config.ShuttingDown || (Queues.CountFeeds > 0)) // Empty the Queue on Shutdown
			{
				Uri url = null;

				try
				{
					url = Queues.GetFeedUrl();
				}
				catch (InvalidOperationException)
				{
					int wait = Config.DefaultWait;
					log.WarnFormat("FeedUrl Queue is empty. Sleeping {0}", wait.ToString(), Thread.CurrentThread.GetHashCode().ToString());
					Thread.Sleep(wait);
					continue;
				}
				Data.InsertFeed(url);
			}
		}
	}
}
