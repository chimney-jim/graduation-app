using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GraduateAppProcessing
{
    public partial class ApplicationForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
            //On page_load, load application (appdetails and prereq sections) + comments based on application ID
            //Save application only saves appdetails and prereq section
            //Submit comment only saves comments section
            //

            if (!IsPostBack)
            {
                lblAppDetailsErrors.Visible = false;
                


                fillListBox();
                bindDataListView(); //comments section

                bindDDLProgram();   //SWE, CSE, IT
            }
        }


        private void saveButtonsTopBottom()
        {
            //datetime.tryparse() -> returns true if parse is successful. False otherwise
            DateTime dt;
            if (String.IsNullOrEmpty(txtDateSubmitted.Text) ||
                     String.IsNullOrEmpty(txtFirstName.Text) ||
                     String.IsNullOrEmpty(txtLastName.Text) ||
                     String.IsNullOrEmpty(txtYear.Text))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Date submitted, first and last name, and year are required fields!";
            }

            else if (!DateTime.TryParse(txtDateSubmitted.Text, out dt))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "The correct date format is mm-dd-yyyy";
            }

            else if (!Regex.IsMatch(txtFirstName.Text, "[-'a-zA-Z]"))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "First Name can only be in letters!";
            }

            else if (!Regex.IsMatch(txtLastName.Text, "[-'a-zA-Z]"))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Last name can only be in letters!";
            }

            else if (!Regex.IsMatch(txtYear.Text, "^[0-9]+$"))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Year has to be in numbers!";
            }

            else
            {
                lblAppDetailsErrors.Text = "";
                lblAppDetailsErrors.Visible = false;

                DateTime lastReviewed = DateTime.Today;
                
                //save prereq in listboxes PC,PT,RQ,NN,RE into crosswalk appprereq
                //based on / using appID, prereqID (and prereqstatusID -> enum)
            }

        }


        #region EVENTS
        
        protected void btnMovePC_Click(object sender, EventArgs e)
        {
            try
            {
                ListItem temp = new ListItem(lbPreRequiredCourses.SelectedItem.Text,
                                             lbPreRequiredCourses.SelectedValue);
                lbPC.Items.Add(temp);
                lbPreRequiredCourses.Items.RemoveAt(lbPreRequiredCourses.SelectedIndex);
            }
            catch(NullReferenceException nullRefEx)
            {
                //do something with nullRefEx
            }
        }

        protected void btnMovePT_Click(object sender, EventArgs e)
        {
            try
            {
                ListItem temp = new ListItem(lbPreRequiredCourses.SelectedItem.Text,
                                             lbPreRequiredCourses.SelectedValue);
                lbPT.Items.Add(temp);
                lbPreRequiredCourses.Items.RemoveAt(lbPreRequiredCourses.SelectedIndex);
            }
            catch (NullReferenceException nullRefEx)
            {
                //do something with nullRefEx
            }
        }

        protected void btnMoveRQ_Click(object sender, EventArgs e)
        {
            try
            {
                ListItem temp = new ListItem(lbPreRequiredCourses.SelectedItem.Text,
                                             lbPreRequiredCourses.SelectedValue);
                lbRQ.Items.Add(temp);
                lbPreRequiredCourses.Items.RemoveAt(lbPreRequiredCourses.SelectedIndex);
            }
            catch (NullReferenceException nullRefEx)
            {
                //do something with nullRefEx
            }
        }

        protected void btnMoveNN_Click(object sender, EventArgs e)
        {
            try
            {
                ListItem temp = new ListItem(lbPreRequiredCourses.SelectedItem.Text,
                                             lbPreRequiredCourses.SelectedValue);
                lbNN.Items.Add(temp);
                lbPreRequiredCourses.Items.RemoveAt(lbPreRequiredCourses.SelectedIndex);
            }
            catch (NullReferenceException nullRefEx)
            {
                //do something with nullRefEx
            }
        }

        protected void btnMoveRE_Click(object sender, EventArgs e)
        {
            try
            {
                ListItem temp = new ListItem(lbPreRequiredCourses.SelectedItem.Text,
                                             lbPreRequiredCourses.SelectedValue);
                lbRE.Items.Add(temp);
                lbPreRequiredCourses.Items.RemoveAt(lbPreRequiredCourses.SelectedIndex);
            }
            catch (NullReferenceException nullRefEx)
            {
                //do something with nullRefEx
            }
        }
        
        protected void btnClearPC_Click(object sender, EventArgs e)
        {
            List<ListItem> tempList = new List<ListItem>();
            
            foreach (ListItem item in lbPC.Items)
            {
                lbPreRequiredCourses.Items.Add(item);
            }

            lbPC.Items.Clear();

            foreach (ListItem item in lbPreRequiredCourses.Items)
            {
                tempList.Add(item);
            }

            lbPreRequiredCourses.Items.Clear();

            foreach (ListItem item in sortList(tempList))
            {
                lbPreRequiredCourses.Items.Add(item);
            }
        }

        protected void btnClearPT_Click(object sender, EventArgs e)
        {
            List<ListItem> tempList = new List<ListItem>();

            foreach (ListItem item in lbPT.Items)
            {
                lbPreRequiredCourses.Items.Add(item);
            }

            lbPT.Items.Clear();

            foreach (ListItem item in lbPreRequiredCourses.Items)
            {
                tempList.Add(item);
            }

            lbPreRequiredCourses.Items.Clear();

            foreach (ListItem item in sortList(tempList))
            {
                lbPreRequiredCourses.Items.Add(item);
            }
        }

        protected void btnClearRQ_Click(object sender, EventArgs e)
        {
            List<ListItem> tempList = new List<ListItem>();

            foreach (ListItem item in lbRQ.Items)
            {
                lbPreRequiredCourses.Items.Add(item);
            }

            lbRQ.Items.Clear();

            foreach (ListItem item in lbPreRequiredCourses.Items)
            {
                tempList.Add(item);
            }

            lbPreRequiredCourses.Items.Clear();

            foreach (ListItem item in sortList(tempList))
            {
                lbPreRequiredCourses.Items.Add(item);
            }
        }

        protected void btnClearNN_Click(object sender, EventArgs e)
        {
            List<ListItem> tempList = new List<ListItem>();

            foreach (ListItem item in lbNN.Items)
            {
                lbPreRequiredCourses.Items.Add(item);
            }

            lbNN.Items.Clear();

            foreach (ListItem item in lbPreRequiredCourses.Items)
            {
                tempList.Add(item);
            }

            lbPreRequiredCourses.Items.Clear();

            foreach (ListItem item in sortList(tempList))
            {
                lbPreRequiredCourses.Items.Add(item);
            }
        }

        protected void btnClearRE_Click(object sender, EventArgs e)
        {
            List<ListItem> tempList = new List<ListItem>();

            foreach (ListItem item in lbRE.Items)
            {
                lbPreRequiredCourses.Items.Add(item);
            }

            lbRE.Items.Clear();

            foreach (ListItem item in lbPreRequiredCourses.Items)
            {
                tempList.Add(item);
            }

            lbPreRequiredCourses.Items.Clear();

            foreach (ListItem item in sortList(tempList))
            {
                lbPreRequiredCourses.Items.Add(item);
            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clear all 5 listboxes (PC, PT, RQ, NN, RE)
            lbPC.Items.Clear();
            lbPT.Items.Clear();
            lbRQ.Items.Clear();
            lbNN.Items.Clear();
            lbRE.Items.Clear();

            if (ddlProgram.SelectedItem.Text.Contains("SELECT ONE"))
            { lbPreRequiredCourses.Items.Clear(); }

            else if (!ddlProgram.SelectedItem.Text.Contains("SELECT ONE"))
            {
                try
                {
                    lbPreRequiredCourses.Items.Clear();
                    ConnectionStringClass connString = new ConnectionStringClass();

                    using (MySqlConnection conn = new MySqlConnection(connString.ConnStr2))
                    using (MySqlCommand cmd = new MySqlCommand("getPrerequisiteCourses", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("@progid", ddlProgram.SelectedValue));
                        conn.Open();

                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            //List<String> listOfString = new List<String>();
                            while (rdr.Read())
                            {
                                lbPreRequiredCourses.Items.Add(new ListItem(rdr["pre-requisite"].ToString()));
                            }
                        }
                    }
                }
                catch (MySqlException mysqlEx)
                {
                    lblErrorApplicationForm.Text = "Error ddlProgram_SelectedIndexChanged: (" + mysqlEx.Message + ")";
                }
                catch (NullReferenceException nullRefEx)
                {
                    lblErrorApplicationForm.Text = "Error ddlProgram_SelectedIndexChanged: (" + nullRefEx.Message + ")";
                }
                catch (Exception ex)
                {
                    lblErrorApplicationForm.Text = "Error ddlProgram_SelectedIndexChanged: General error (" + ex.Message + ")";
                }
            }//END IF
        }

        #endregion

        #region MISC

        private List<ListItem> sortList(List<ListItem> items)
        {
            List<ListItem> tempList = new List<ListItem>();
            foreach (ListItem item in items.OrderBy(item => item.Text))
            {
                tempList.Add(item);
            }
            return tempList;
        }

        //
        //Sample values for listbox transition course
        //TODO: get actual data from MySQL (bind the datasource to the control)~~~~~
        //
        private void fillListBox()
        {
            lbPreRequiredCourses.Items.Clear();
            ListItem[] TransitioncourseList = new ListItem[10];
            TransitioncourseList[0] = new ListItem("Sample course 1", "sample");
            TransitioncourseList[1] = new ListItem("course 2", "aloha");
            TransitioncourseList[2] = new ListItem("Sample course 3");
            TransitioncourseList[3] = new ListItem("Sample course 4");
            TransitioncourseList[4] = new ListItem("Sample course 5a");
            TransitioncourseList[5] = new ListItem("Sample course 6");
            TransitioncourseList[6] = new ListItem("Sample course 7");
            TransitioncourseList[7] = new ListItem("Sample course 8");
            TransitioncourseList[8] = new ListItem("Sample course 9");
            TransitioncourseList[9] = new ListItem("Sample course 10");
            //TODO~~~~~~

            List<ListItem> li = new List<ListItem>(TransitioncourseList);
            //add SORTED items into listbox one by one
            foreach (ListItem item in sortList(li))
            {
                lbPreRequiredCourses.Items.Add(item);
            }
        }

        //test: bind data source MySQL to server control ListView (comments section)
        protected void bindDataListView()
        {
            try
            {
                ConnectionStringClass connStrings = new ConnectionStringClass();
                List<Application> listOfAppObj = new List<Application>();

                using (MySqlConnection conn = new MySqlConnection(connStrings.ConnStr1))
                using (MySqlCommand cmd = new MySqlCommand("GetTestListView", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        Application appObj;
                        while (rdr.Read())
                        {
                            appObj = new Application();
                            appObj.CommentId = Convert.ToInt32(rdr["id"].ToString());
                            appObj.CommentBody = rdr["commentBody"].ToString();
                            listOfAppObj.Add(appObj);
                        }
                    }
                }
                lvComment.DataSource = listOfAppObj;
                lvComment.DataBind();
            }
            catch (NullReferenceException nullRefEx)
            {
                lblErrorApplicationForm.Text = "Error bindDataListView: (" + nullRefEx.Message + ")";
            }
            catch (MySqlException mysqlEx)
            {
                lblErrorApplicationForm.Text = "Error bindDataListView: (" + mysqlEx.Message + ")";
            }
            catch (Exception ex)
            {
                lblErrorApplicationForm.Text = "Error bindDataListView: General error (" + ex.Message + ")";
            }
        }

        protected void bindDDLProgram()
        {
            ddlProgram.Items.Insert(0, "----SELECT ONE----");
            try
            {
                ConnectionStringClass connStrings = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStrings.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("getDDLProgram", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {                                        //text,                    value
                            ddlProgram.Items.Add(new ListItem(rdr["Name"].ToString(), rdr["Id"].ToString()));
                        }
                    }
                }
            }
            catch (MySqlException mysqlEx)
            {
                lblErrorApplicationForm.Text = "Error bindDDLProgram: (" + mysqlEx.Message + ")";
            }
            catch (NullReferenceException nullRefEx)
            {
                lblErrorApplicationForm.Text = "Error bindDDLProgram: (" + nullRefEx.Message + ") , not connected to db";
            }
            catch (Exception ex)
            {
                lblErrorApplicationForm.Text = "Error bindDDLProgram: General error (" + ex.Message + ")";
            }
        }

        #endregion

        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            //txtTestComment.Text = txtComment.Text;
            String currentComment = txtComment.Text; //limit to 512

            ConnectionStringClass connString = new ConnectionStringClass();

            using (MySqlConnection conn = new MySqlConnection(connString.ConnStr2))
            using (MySqlCommand cmd = new MySqlCommand("", conn))
            {
                //TODO: SQL query
                //Stored Procedure: SELECT comments based on appId
                //
            }
        }

        protected void btnSaveBottom_Click(object sender, EventArgs e)
        {
            saveButtonsTopBottom();
        }

        protected void btnSaveTop_Click(object sender, EventArgs e)
        {
            saveButtonsTopBottom();
        }
    }
}