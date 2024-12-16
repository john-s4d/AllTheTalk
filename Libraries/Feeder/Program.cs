using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace AllTheTalk.Feeder
{
	public class Program
	{
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public static void PreInitialize()
		{
			// TODO: Get Log Config from central location.
			FileInfo logConfigFile = new FileInfo("C:\\Documents and Settings\\John\\My Documents\\Visual Studio 2005\\Projects\\AllTheTalk\\Libraries\\Feeder\\logging.config");
			log4net.Config.XmlConfigurator.ConfigureAndWatch(logConfigFile);
		}

		public static void Initialize()
		{
			Config.ShuttingDown = false; // TODO: Reset app configuration from default file.
			// TODO: Check if there is any data from a previous shut down
		}

		public static void ShutDown()
		{
			log.Info("Begin Application Shutdown");
			Config.ShuttingDown = true;
			// TODO: Monitor (force) Shutdown procedure
			// TODO: Empty any remaining queues to a local file
		}

	}
}
