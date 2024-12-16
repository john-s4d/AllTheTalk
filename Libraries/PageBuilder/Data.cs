using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace CategorizerLib
{
	public class Data
	{
		private static String _connStr;

		// Static Constructor
		static Data()
		{
			SetConnectionString(Config.DbConnectString);
		}

		private static void SetConnectionString(String ConnectionString)
		{
			_connStr = ConnectionString;
		}

		public static DataSet GetNewItems(DataSet dataset, int count, int maxAttempts)
		{
			String cmdStr = "SELECT itemID, topicID, categoryID, pageID, catAttempts, ItemTitle, ItemContentEncoded FROM feeditems WHERE pageID IS NULL and (catAttempts < ?maxAttempts OR catAttempts IS NULL) LIMIT ?count";

			MySqlConnection conn = new MySqlConnection(_connStr);
			MySqlDataAdapter adapter = new MySqlDataAdapter();
			adapter.SelectCommand = new MySqlCommand(cmdStr, conn);
			adapter.SelectCommand.Parameters.AddWithValue("maxAttempts", maxAttempts);
			adapter.SelectCommand.Parameters.AddWithValue("count", count);	

			adapter.Fill(dataset);
			return dataset;			
		}	
	}
}
