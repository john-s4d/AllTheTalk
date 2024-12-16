using System;
using System.Collections.Generic;
using System.Text;

namespace AllTheTalk.Crawl
{
	public class Config
	{
		private static int _batchSize = 256;
		private static int _maxThreads = 256;
		private static int _hostReloadThreshold = 32;
		private static Boolean _shuttingDown = false;
		private static int _crawlerWaitTime = 1000;
		private static String _crawlExcludeExts = "css, js, jpg, jpeg, gif, png, mpeg, mov, wmv, wma, zip, pdf";
		private static String _crawlIncludeExts = "htm, html, asp, aspx, php, cfm";
		private static String _crawlExcludePrefix = "javascript, mailto, ftp";
		private static String _crawlIncludePrefix = "http, https";
		private static String _dbConnectString = "Server=ikailo;Database=allthetalk;Uid=allthetalk;Pwd=att123!@#";
		private static int _defaultWait = 10000;

		public static int BatchSize
		{
			get { return _batchSize; }
			set { _batchSize = value; }
		}

		public static int MaxThreads
		{
			get { return _maxThreads; }
			set { _maxThreads = value; }
		}

		public static int HostReloadThreshold
		{
			get { return _hostReloadThreshold; }
			set { _hostReloadThreshold = value; }
		}

		public static Boolean ShuttingDown
		{
			get { return _shuttingDown; }
			set { _shuttingDown = value; }
		}

		public static int CrawlerWaitTime
		{
			get { return _crawlerWaitTime; }
			set { _crawlerWaitTime = value; }
		}

		public static String CrawlExcludeExts
		{
			get { return _crawlExcludeExts; }
			set { _crawlExcludeExts = value; }
		}

		public static String CrawlIncludeExts
		{
			get { return _crawlIncludeExts; }
			set { _crawlIncludeExts = value; }
		}

		public static String CrawlExcludePrefix
		{
			get { return _crawlExcludePrefix; }
			set { _crawlExcludePrefix = value; }
		}

		public static String CrawlIncludePrefix
		{
			get { return _crawlIncludePrefix; }
			set { _crawlIncludePrefix = value; }
		}

		public static String DbConnectString
		{
			get { return _dbConnectString; }
			set { _dbConnectString = value; }
		}

		public static int DefaultWait
		{
			get { return _defaultWait; }
			set { _defaultWait = value; }
		}

		
		static Config()
		{
		}
	}
}
