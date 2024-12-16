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
using MySql.Data.MySqlClient;

namespace AllTheTalk.Web.Controls {

	public partial class NewlyAddedArticles : System.Web.UI.UserControl {

		protected void Page_Load(object sender, EventArgs e) {

		}

		private void LoadArticles(int Quantity) {

			String connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AllTheTalk"];
			String commandText = @"";

			MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, connString);

		}

	}
}