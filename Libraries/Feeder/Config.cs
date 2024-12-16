using System;
using System.Collections.Generic;
using System.Text;

namespace AllTheTalk.Feeder
{
	public static class Config
	{
		private static Boolean _shuttingDown = false;
		private static String _dbConnectString = "Server=ikailo;Database=allthetalk;Uid=allthetalk;Pwd=att123!@#";

		public static Boolean ShuttingDown
		{
			get { return _shuttingDown; }
			set { _shuttingDown = value; }
		}

		public static String DbConnectString
		{
			get { return _dbConnectString; }
			set { _dbConnectString = value; }
		}

		static Config()
		{
		}
	}
}
