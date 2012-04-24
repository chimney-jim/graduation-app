using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GraduateAppProcessing
{
    public partial class Error_ViewApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "Error: No record found in Database (application). Redirecting in 5...";
            Response.AddHeader("REFRESH", "5;URL=Default.aspx");
        }
    }
}