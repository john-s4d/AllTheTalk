using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using log4net;
using Rss;

namespace AllTheTalk.Feeder
{
	internal static class Data
	{
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static String _connStr;

		// FIXME: Optimize all Database Transactions

		// Static Constructor
		static Data()
		{
			SetConnectionString(Config.DbConnectString);
		}

		private static void SetConnectionString(String ConnectionString)
		{
			_connStr = ConnectionString;
		}

		internal static FeedToProcess GetFeedToProcess()
		{

			// Get the oldest item from the database, ensure it has not been visited in the past 12 hours.
			DateTime nowdate = DateTime.UtcNow;
			
			String cmdStr = "SELECT Url,  FROM feeds WHERE DateLastVisited > ?ThresholdDate ORDER BY DateLastVisited LIMIT 1";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
			cmd.Parameters.AddWithValue("ThresholdDate", nowdate.AddHours(-12));

			String url = (String)cmd.ExecuteReader();

			Uri uri = new Uri(url);

			return uri;

		}

		internal static bool ChannelWasUpdated(RssChannel Channel)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		internal static void UpsertChannel(RssChannel Channel)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		internal static bool ItemIsOutDated(RssItem Item)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		internal static bool ItemExists(RssItem Item)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		internal static void InsertItem(RssItem Item)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		internal static void UpdateItem(RssItem Item)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
