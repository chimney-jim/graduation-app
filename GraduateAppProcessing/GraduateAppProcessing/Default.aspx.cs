using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics; //to use Debug.Write()
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GraduateAppProcessing
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //////
            //////Test MySQL connection (with stored procedure), *worked*
            //////
            ////ConnectionStringClass connStrings = new ConnectionStringClass();
            
            ////using (MySqlConnection conn = new MySqlConnection(connStrings.connStr))
            ////using (MySqlCommand cmd = conn.CreateCommand())
            ////{
            ////    conn.Open();
            ////    cmd.CommandType = CommandType.StoredProcedure;
            ////    cmd.CommandText = "GetTestTable";
            ////    using (MySqlDataReader rdr = cmd.ExecuteReader())
            ////    {
            ////        while (rdr.Read())
            ////        {
            ////            //test textboxes
            ////            TextBox1.Text = rdr[0].ToString();
            ////            TextBox2.Text = rdr[1].ToString();
            ////        }
            ////    }
            ////}   
        }

        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            int appNumber = 1;
            Response.Redirect("ViewApplication.aspx?appNumber=" + appNumber.ToString());
        }
    }
}
