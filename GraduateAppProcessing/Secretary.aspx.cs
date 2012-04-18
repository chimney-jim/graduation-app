using System;
using System.Object;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GraduateAppProcessing
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private String selected;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void editAppButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationForm.aspx", true);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            selected = Convert.ToString(row.Cells[0].Text);
        }

        protected void addAppButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationForm.aspx", true);
        }
    }
}