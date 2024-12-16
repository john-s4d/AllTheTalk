using System;
using System.Collections.Generic;
using System.Text;

namespace AllTheTalk.Crawl
{
	public class Queues
	{
		private static Queue<Uri> _feedUrls;
		private static Queue<Uri> _localUrls;
		private static Queue<Uri> _remoteUrls;

		// Static Constructor
		static Queues()
		{
			_feedUrls = new Queue<Uri>();
			_localUrls = new Queue<Uri>();
			_remoteUrls = new Queue<Uri>();
		}

		#region Feed Queue

		private static object feedLock = new Object();

		public static void AddFeed(Uri Uri)
		{
			lock (feedLock)
			{
				_feedUrls.Enqueue(Uri);
			}
		}

		public static Uri GetFeedUrl()
		{
			lock (feedLock)
			{
				return _feedUrls.Dequeue();
			}
		}

		public static int CountFeeds
		{
			get
			{
				lock (feedLock) { return _feedUrls.Count; }
			}
		}

		#endregion

		#region Local Url Queue

		private static object localUrlLock = new Object();

		public static void AddLocalUrl(Uri Uri)
		{
			lock (localUrlLock)
			{
				_localUrls.Enqueue(Uri);
			}
		}

		public static Uri GetLocalUrl()
		{
			lock (localUrlLock)
			{
				return _localUrls.Dequeue();
			}
		}

		public static int CountLocalUrls
		{
			get
			{
				lock (localUrlLock) { return _localUrls.Count; }
			}
		}

		#endregion

		#region Remote Url Queue

		private static object remoteUrlLock = new Object();

		public static void AddRemoteUrl(Uri Uri)
		{
			lock (remoteUrlLock)
			{
				_remoteUrls.Enqueue(Uri);
			}
		}

		public static Uri GetRemoteUrl()
		{
			lock (remoteUrlLock)
			{
				return _remoteUrls.Dequeue();
			}
		}

		public static int CountRemoteUrls
		{
			get
			{
				lock (remoteUrlLock) { return _remoteUrls.Count; }
			}
		}

		#endregion

	}
}

