using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AllTheTalk.Crawl
{
	public class Util
	{
		/// <summary>
		/// Removes duplicate rows from a DataTable
		/// </summary>
		/// <param name="table">DataTable to de-duplicate</param>
		/// <param name="keyColumns">Key Columns to check for duplicates</param>
		public static void RemoveDuplicates(DataTable table, List<string> keyColumns)
		{
			Dictionary<string, string> uniquenessDict = new Dictionary<string, string>(table.Rows.Count);
			StringBuilder sb = null;
			int rowIndex = 0;
			DataRow row;
			DataRowCollection rows = table.Rows;
			while (rowIndex < rows.Count - 1)
			{
				row = rows[rowIndex];
				sb = new StringBuilder();
				foreach (string colname in keyColumns)
				{
					sb.Append(((string)row[colname]));
				}

				if (uniquenessDict.ContainsKey(sb.ToString()))
				{
					rows.Remove(row);
				}
				else
				{
					uniquenessDict.Add(sb.ToString(), string.Empty);
					rowIndex++;
				}
			}

		}
	}
}
