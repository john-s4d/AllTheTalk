using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using log4net;
using System.Threading;

namespace AllTheTalk.Crawl
{
	public class Data
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

		public static Uri GetHostToProcess()
		{
			// Get the oldest item from the database
			DateTime nowdate = DateTime.UtcNow;
			DateTime thresholdDate = nowdate.AddDays(-1);

			// FIXME: This doesn't work. Need to select the oldest item from the queue only ONE time..

			String cmdStr = "SELECT Url FROM hosts WHERE (DateLastCompleteBrowse > DateLastRetrieved) OR (DateLastRetrieved IS NULL) " +
				"AND (Excluded IS NOT True) ORDER BY DateLastVisited LIMIT 1";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
			cmd.Parameters.AddWithValue("ThresholdDate", thresholdDate);
			cmd.Parameters.AddWithValue("NowDate", nowdate);

			String url = (String)cmd.ExecuteScalar();

			Uri uri = new Uri(url);
			
			return uri;

		}

		public static int GetHostId(Uri Host)
		{
			String hostUrl = Host.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);

			log.DebugFormat("Begin retrieving Id for Host {0} from DB.", hostUrl);

			String cmdStr = "SELECT Id FROM Hosts WHERE Url = ?url";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
			cmd.Parameters.AddWithValue("url", hostUrl);

			conn.Open();
			int Id = (int)cmd.ExecuteScalar();
			conn.Close();

			log.DebugFormat("Retrieved Id {0} for Host {1} from DB.", Id.ToString(), hostUrl);

			return Id;
		}

		private static bool HostExists(String HostUrl)
		{
			log.DebugFormat("Begin checking if host {0} exists in DB.", HostUrl);

			String cmdStr = "SELECT COUNT(*) FROM hosts WHERE Url = ?url";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
			cmd.Parameters.AddWithValue("url", HostUrl);

			conn.Open();
			Int64 count = (Int64)cmd.ExecuteScalar();
			conn.Close();

			log.DebugFormat("Host {0} {1} in DB.", HostUrl, (count > 0) ? "exists" : "does not exist");

			if (count > 0)
				return true;

			return false;
		}

		private static bool DeepLinkExists(string LinkUrl)
		{
			log.DebugFormat("Begin checking if DeepLink {0} exists in DB.", LinkUrl);

			String cmdStr = "SELECT Count(*) FROM deeplinks WHERE Url = ?url";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
			cmd.Parameters.AddWithValue("url", LinkUrl);
			
			conn.Open();
			Int64 count = (Int64)cmd.ExecuteScalar();
			conn.Close();

			log.DebugFormat("DeepLink {0} {1} in DB.", LinkUrl, (count > 0) ? "exists" : "does not exist");

			if (count > 0)
				return true;

			return false;
		}

		private static bool FeedExists(string FeedUrl)
		{
			log.DebugFormat("Begin checking if Feed {0} exists in DB.", FeedUrl);

			String cmdStr = "SELECT Count(*) FROM feeds WHERE Url = ?url";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
			cmd.Parameters.AddWithValue("url", FeedUrl);

			int count = (int)cmd.ExecuteScalar();

			log.DebugFormat("Feed {0} {1} in DB.", FeedUrl, (count > 0) ? "exists" : "does not exist");

			if (count > 0)
				return true;

			return false;
		}

		public static void InsertHost(Uri Host)
		{
			String hostUrl = Host.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);

			log.DebugFormat("Begin inserting host {0} to DB.", hostUrl);

			if (!HostExists(hostUrl)) // TODO: Optimize DB calls.
			{
				String cmdStr = "INSERT INTO hosts (Url, DateAdded) VALUES (?url, ?date)";

				MySqlConnection conn = new MySqlConnection(_connStr);
				MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
				cmd.Parameters.AddWithValue("url", hostUrl);
				cmd.Parameters.AddWithValue("date", DateTime.UtcNow);
				
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();
			}
			log.DebugFormat("End inserting Host {0} to DB.", hostUrl);
		}

		public static void InsertDeepLink(Uri Link)
		{
			String linkUrl = Link.ToString();
			log.DebugFormat("Begin inserting DeepLink {0} to DB.", linkUrl);

			if (!DeepLinkExists(linkUrl))
			{
				String hostUrl = Link.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
				String cmdStr = "INSERT INTO deeplinks (HostUrl, Url, DateAdded) VALUES (?hostUrl, ?url, ?date)";

				MySqlConnection conn = new MySqlConnection(_connStr);
				MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
				cmd.Parameters.AddWithValue("hostUrl", hostUrl);
				cmd.Parameters.AddWithValue("url", linkUrl);
				cmd.Parameters.AddWithValue("date", DateTime.UtcNow);

				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();
			}
			log.DebugFormat("End inserting DeepLink {0} to DB.", linkUrl);
		}

		public static void DeleteDeepLink(Uri Link)
		{
			String linkUrl = Link.ToString();

			log.DebugFormat("Begin deleting DeepLink {0} to DB.", linkUrl);

			String cmdStr = "DELETE FROM deeplinks WHERE Url = ?url";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
			cmd.Parameters.AddWithValue("url", linkUrl);

			conn.Open();
			int count = cmd.ExecuteNonQuery();
			conn.Close();

			log.DebugFormat("Deleted {1} DeepLink {0} from DB.", linkUrl, count.ToString());

		}

		public static void InsertFeed(Uri Feed)
		{
			String feedUrl = Feed.ToString();
			log.DebugFormat("Begin inserting Feed {0} to DB.", feedUrl);

			if (!FeedExists(feedUrl))
			{
				int hostId = GetHostId(Feed);
				String cmdStr = "INSERT INTO feeds (HostId, Url, DateAdded) VALUES (?hostId, ?url, ?date)";

				MySqlConnection conn = new MySqlConnection(_connStr);
				MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
				cmd.Parameters.AddWithValue("hostId", hostId);
				cmd.Parameters.AddWithValue("url", feedUrl);
				cmd.Parameters.AddWithValue("date", DateTime.UtcNow);
				
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();
			}
			log.DebugFormat("End inserting Feed {0} to DB.", feedUrl);
		}

		public static void UpdateHostVisited(Uri Host)
		{
			String hostUrl = Host.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);

			log.DebugFormat("Begin updating last visited host {0}", hostUrl);

			// Update Host Visited
			String cmdStr = "UPDATE hosts SET DateLastVisited = ?date WHERE Url = ?hostUrl";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlCommand cmd = new MySqlCommand(cmdStr, conn);
			cmd.Parameters.AddWithValue("hostUrl", hostUrl);
			cmd.Parameters.AddWithValue("date", DateTime.UtcNow);

			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();

			log.DebugFormat("End updating last visited host {0}", hostUrl);

			/* TODO: Validate remaining DeepLinks
			 * This should be done only after we can be sure the local queue has been cleared.
			
			log.DebugFormat("Begin updating verified deeplinks for host {0}", hostUrl);

			cmd.CommandText = "UPDATE deeplinks SET Verified = True, DateVerified = ?date WHERE HostUrl = ?hostUrl";
			cmd.ExecuteNonQuery();

			log.DebugFormat("End updating verified deeplinks for host {0}", hostUrl);			
			 */
		}
	}
}
