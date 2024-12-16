using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using log4net;

namespace AllTheTalk.Crawl
{
	public class Program
	{
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public static void PreInitialize()
		{
			// TODO: Logger Wrapper
			FileInfo logConfigFile = new FileInfo("C:\\Documents and Settings\\John\\My Documents\\Visual Studio 2005\\Projects\\AllTheTalk\\Libraries\\Crawler\\logging.config");
			log4net.Config.XmlConfigurator.ConfigureAndWatch(logConfigFile);
		}

		public static void Initialize()
		{
			Config.ShuttingDown = false; 
			// TODO: Reset app configuration from default config file.
			// TODO: Check if there is any data from a previous shut down
		}
		
		public static void ShutDown()
		{
			log.Info("Begin Application Shutdown");
			Config.ShuttingDown = true;
			// TODO: Monitor (force) Shutdown procedure
			// TODO: Empty any remaining queues to a local file
		}

		// FIXME: Change debugging handling
		private static Boolean _debugMode = false;
		private static int _hostsToProcess = 0;
		private static int _processedHosts = 0;
		private static Boolean _shutDownFlag = false;
		private void SetDebugMode(Boolean DebugMode, int HostsToProcess)
		{
			if (DebugMode)
			{
				log.DebugFormat("Debug mode enabled - processing {0} Hosts.", HostsToProcess);
				_debugMode = DebugMode;
				_hostsToProcess = HostsToProcess;
				Config.BatchSize = HostsToProcess;
			}
		}
	}
}
