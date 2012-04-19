using System;
using System.Collections.Generic;
using System.Data;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblSecretaryError.Text = String.Empty;
                lblSecretaryError.Visible = false;
                Session["SortOrder"] = "ASC";   //initial sort value for gridview
                LoadApplicationsToGridView();
                
            }
            else
            {
                lblSecretaryError.Text = String.Empty;
                lblSecretaryError.Visible = false;
            }
            
        }

        protected void addAppButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationForm.aspx");
        }

        private void LoadApplicationsToGridView()
        {
            DataTable dTable = new DataTable();
            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("LoadApplicationsToGridView", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (MySqlDataAdapter dAdapter = new MySqlDataAdapter())
                    {
                        dAdapter.SelectCommand = cmd;
                        conn.Open();
                        dAdapter.Fill(dTable);
                        //Cache["DataTableApp"] = dTable;
                        Session["DataTableGV"] = dTable;
                        gvApplications.DataSource = (DataTable)this.Session["DataTableGV"];
                        gvApplications.DataBind();
                    }
                }
            }
            catch (MySqlException mysqlEx)
            {
                lblSecretaryError.Visible = true;
                lblSecretaryError.Text = "Bind gridview Applications Error: " + mysqlEx.Message;
            }
            catch (NullReferenceException nullRefEx)
            {
                lblSecretaryError.Visible = true;
                lblSecretaryError.Text = "Null Reference (gridview) Error: " + nullRefEx.Message;
            }
            catch (Exception Ex)
            {
                lblSecretaryError.Visible = true;
                lblSecretaryError.Text = "General gridview binding Error: " + Ex.Message;
            }
        }

        protected void gvApplications_Sorting(object sender, GridViewSortEventArgs e)
        {
            //DataView dv = gvApplications.DataSource as DataView;
            DataTable dt = (DataTable)Session["DataTableGV"];
            String columnName = e.SortExpression;
            
            if (Session["SortOrder"].ToString().Equals("ASC"))
            {
                dt.DefaultView.Sort = columnName + " DESC";
                Session["SortOrder"] = "DESC";
            }
            else if (Session["SortOrder"].ToString().Equals("DESC"))
            {
                dt.DefaultView.Sort = columnName + " ASC";
                Session["SortOrder"] = "ASC";
            }
            
            Session["DataTableGV"] = dt;
            gvApplications.DataSource = Session["DataTableGV"];
            gvApplications.DataBind();
            columnName = String.Empty;
        }

        

        protected void gvApplications_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvApplications.PageIndex = e.NewPageIndex;
                //LoadApplicationsToGridView();
                //gvApplications.DataSource = (DataTable)Cache["DataTableApp"];
                gvApplications.DataSource = (DataTable)Session["DataTableGV"];
                //Cache["DataTableApp"] = null;
                gvApplications.DataBind();
            }
            catch (Exception Ex)
            {
                lblSecretaryError.Visible = true;
                lblSecretaryError.Text = "Gridview Page Index Error: " + Ex.Message;
            }
        }

        protected void viewAppButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewApplication.aspx");
        }
    }
}