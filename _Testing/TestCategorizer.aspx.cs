using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AllTheTalk.PageBuilder;
using System.IO;

public partial class TestCategorizer : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	private void Run()
	{
		DataTable dt = GetData();
		CategorizerConfig config = new CategorizerConfig(10);
		String[] cols = { "content" };
		String key = "id";
		Categorizer kMeans = new Categorizer(dt, config, cols, key);
		DataSet result = new DataSet();
		result = kMeans.Sort();

		foreach (DataTable table in result.Tables)
		{
			lblMessage.Text += "<strong>" + "Table Found: " + table.TableName + "</strong>" + "<br />";
			foreach (DataColumn column in table.Columns)
			{
				lblMessage.Text += "Column Found: " + column.ColumnName + "<br />";
			}			
			lblMessage.Text += "Row Count: " + table.Rows.Count + "<br />" + "<br />";
		}

		kMeans.Dispose();
		
	}

	private DataTable GetData()
	{

		DataTable dt = new DataTable();
		dt.Columns.Add("id");
		dt.Columns.Add("content");

		String sourceFile = Server.MapPath("~/App_Data/CatData.txt");
		TextReader tr = new StreamReader(sourceFile);

		String line;
		while ((line = tr.ReadLine()) != null)
		{
			DataRow row = dt.NewRow();
			int i = line.IndexOf(" ");
			row["id"] = line.Substring(0,i).Trim();
			row["content"] = line.Substring(i, line.Length - i).Trim();
			dt.Rows.Add(row);
		}
		return dt;		
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		Run();
	}
}
