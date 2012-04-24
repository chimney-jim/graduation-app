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
            if (CheckIf_Atleast_OneApp_Exists())
                lblApplicationIdNumber.Text = SetNewApplicationId().ToString();
            else
                lblApplicationIdNumber.Text = "1";

            if (!IsPostBack)
            {
                lblAppDetailsErrors.Text = String.Empty;
                lblAppDetailsErrors.Visible = false;
                lblPreReqErrors.Text = String.Empty;
                lblPreReqErrors.Visible = false;

                //appdetails section
                PopulateDDLProgram();   //SWE, CSE, IT
                PopulateDDLTerm();                
                //lastViewedDate();

                //prereq section
                //FillListBox();

                //comments section
                //BindDataListView();

                
            }
        }


        #region EVENTS

        protected void btnSaveBottom_Click(object sender, EventArgs e)
        {
            SaveButtonsTopBottom();
            //Response.Redirect("ApplicationForm.aspx");
        }

        protected void btnSaveTop_Click(object sender, EventArgs e)
        {
            SaveButtonsTopBottom();
            //Response.Redirect("ApplicationForm.aspx");
        }
        
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

            foreach (ListItem item in SortList(tempList))
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

            foreach (ListItem item in SortList(tempList))
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

            foreach (ListItem item in SortList(tempList))
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

            foreach (ListItem item in SortList(tempList))
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

            foreach (ListItem item in SortList(tempList))
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
                                lbPreRequiredCourses.Items.Add(new ListItem(rdr["pre-requisite"].ToString(),
                                                                            rdr["Id"].ToString()));
                            }
                        }
                        //txtFirstName.Text = ddlProgram.SelectedValue;
                    }
                }
                catch (MySqlException mysqlEx)
                {
                    lblAppDetailsErrors.Visible = true;
                    lblAppDetailsErrors.Text = "Error ddlProgram_SelectedIndexChanged: (" + mysqlEx.Message + ")";
                }
                catch (NullReferenceException nullRefEx)
                {
                    lblAppDetailsErrors.Visible = true;
                    lblAppDetailsErrors.Text = "Error ddlProgram_SelectedIndexChanged: (" + nullRefEx.Message + ")";
                }
                catch (Exception ex)
                {
                    lblAppDetailsErrors.Visible = true;
                    lblAppDetailsErrors.Text = "Error ddlProgram_SelectedIndexChanged: General error (" + ex.Message + ")";
                }
            }//END ELSE IF
        }

        


        #endregion

        #region MISC

        private void SavePreReq()
        {
            try
            {
                //PC
                if (lbPC.Items.Count > 0)
                {
                    foreach (ListItem item in lbPC.Items)
                    {
                        ConnectionStringClass connStr = new ConnectionStringClass();

                        using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                        using (MySqlCommand cmd = new MySqlCommand("setAppPreReq", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));
                            //cmd.Parameters.Add(new MySqlParameter("@appId", 3));    //temp app id num = 3
                            cmd.Parameters.Add(new MySqlParameter("@PReqId", item.Value));
                            cmd.Parameters.Add(new MySqlParameter("@PReqStatusId", 1));  //1 = PC

                            conn.Open();
                            //cmd.ExecuteNonQuery();
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                        }
                    }
                }
                //PT
                if (lbPT.Items.Count > 0)
                {
                    foreach (ListItem item in lbPT.Items)
                    {
                        ConnectionStringClass connStr = new ConnectionStringClass();

                        using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                        using (MySqlCommand cmd = new MySqlCommand("setAppPreReq", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));
                            //cmd.Parameters.Add(new MySqlParameter("@appId", 3));    //temp app id num = 3
                            cmd.Parameters.Add(new MySqlParameter("@PReqId", item.Value));
                            cmd.Parameters.Add(new MySqlParameter("@PReqStatusId", 2));  //2 = PT

                            conn.Open();
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                        }
                    }
                }
                //RQ
                if (lbRQ.Items.Count > 0)
                {
                    foreach (ListItem item in lbRQ.Items)
                    {
                        ConnectionStringClass connStr = new ConnectionStringClass();

                        using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                        using (MySqlCommand cmd = new MySqlCommand("setAppPreReq", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));
                            //cmd.Parameters.Add(new MySqlParameter("@appId", 3));    //temp app id num = 3
                            cmd.Parameters.Add(new MySqlParameter("@PReqId", item.Value));
                            cmd.Parameters.Add(new MySqlParameter("@PReqStatusId", 3));  //3 = RQ

                            conn.Open();
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                        }
                    }
                }
                //NN
                if (lbNN.Items.Count > 0)
                {
                    foreach (ListItem item in lbNN.Items)
                    {
                        ConnectionStringClass connStr = new ConnectionStringClass();

                        using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                        using (MySqlCommand cmd = new MySqlCommand("setAppPreReq", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));
                            //cmd.Parameters.Add(new MySqlParameter("@appId", 3));    //temp app id num = 3
                            cmd.Parameters.Add(new MySqlParameter("@PReqId", item.Value));
                            cmd.Parameters.Add(new MySqlParameter("@PReqStatusId", 4));  //4 = NN

                            conn.Open();
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                        }
                    }
                }
                //RE
                if (lbRE.Items.Count > 0)
                {
                    foreach (ListItem item in lbRE.Items)
                    {
                        ConnectionStringClass connStr = new ConnectionStringClass();

                        using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                        using (MySqlCommand cmd = new MySqlCommand("setAppPreReq", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));
                            //cmd.Parameters.Add(new MySqlParameter("@appId", 3));    //temp app id num = 3
                            cmd.Parameters.Add(new MySqlParameter("@PReqId", item.Value));
                            cmd.Parameters.Add(new MySqlParameter("@PReqStatusId", 5));  //5 = RE

                            conn.Open();
                            if (cmd.ExecuteNonQuery() < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                        }
                    }
                }
            }
            catch
            { throw; }
        }

        private void SaveButtonsTopBottom()
        {
            //txtFirstName.Text = DateTime.Today.ToString("yyyy-MM-dd");

            lblAppDetailsErrors.Text = String.Empty;
            lblAppDetailsErrors.Visible = false;


            //datetime.tryparse() -> returns true if parse is successful. False otherwise
            DateTime dt;
            if (String.IsNullOrEmpty(txtDateSubmitted.Text) ||
                     String.IsNullOrEmpty(txtFirstName.Text) ||
                     String.IsNullOrEmpty(txtLastName.Text) ||
                     String.IsNullOrEmpty(txtYear.Text))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Date submitted, first and last name, and year are required fields";
            }

            else if (!DateTime.TryParse(txtDateSubmitted.Text, out dt))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "The correct date format is mm-dd-yyyy";
            }

            else if (!Regex.IsMatch(txtFirstName.Text, "[-'a-zA-Z]"))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "First Name can only be letters";
            }

            else if (!Regex.IsMatch(txtLastName.Text, "[-'a-zA-Z]"))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Last name can only be letters";
            }

            else if (!Regex.IsMatch(txtYear.Text, "^[0-9]+$"))
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Year has to be numbers";
            }

            else if (lbPC.Items.Count < 1 &&
                     lbPT.Items.Count < 1 &&
                     lbRQ.Items.Count < 1 &&
                     lbNN.Items.Count < 1 &&
                     lbRE.Items.Count < 1)
            {
                lblPreReqErrors.Visible = true;
                lblPreReqErrors.Text = "Prerequisite boxes cannot all be empty";
            }

            else
            {
                lblAppDetailsErrors.Text = String.Empty;
                lblAppDetailsErrors.Visible = false;
                lblPreReqErrors.Text = String.Empty;
                lblPreReqErrors.Visible = false;

                DateTime lastReviewedDate = LastViewedDate();

                try
                {
                    ConnectionStringClass connString = new ConnectionStringClass();

                    using (MySqlConnection conn = new MySqlConnection(connString.ConnStr2))
                    using (MySqlCommand cmd = new MySqlCommand("setApplication", conn))     //change SP to 'saveApplication'?
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("@firstN", txtFirstName.Text));
                        cmd.Parameters.Add(new MySqlParameter("@lastN", txtLastName.Text));
                        cmd.Parameters.Add(new MySqlParameter("@progId", ddlProgram.SelectedValue));
                        cmd.Parameters.Add(new MySqlParameter("@term", ddlTerm.SelectedValue));
                        cmd.Parameters.Add(new MySqlParameter("@year", Convert.ToInt32(txtYear.Text)));
                        //convert dateSubmitted to 'yyyy/dd/MM' format
                        cmd.Parameters.Add(new MySqlParameter("@dateInput", DateTime.Parse(txtDateSubmitted.Text).ToString("yyyy-MM-dd")));
                        cmd.Parameters.Add(new MySqlParameter("@app_Date", DateTime.Today.ToString("yyyy-MM-dd")));
                        cmd.Parameters.Add(new MySqlParameter("@lastViewed", DateTime.Today.ToString("yyyy-MM-dd")));
                        if (rbUndecided.Checked)
                            cmd.Parameters.Add(new MySqlParameter("@isAccept", "0"));
                        else if (rbAccepted.Checked)
                            cmd.Parameters.Add(new MySqlParameter("@isAccept", 2));
                        else if (rbDenied.Checked)
                            cmd.Parameters.Add(new MySqlParameter("@isAccept", 1));   //integer 0 is considered null in mysql
                        if (rbFinalizedYes.Checked)
                            cmd.Parameters.Add(new MySqlParameter("@isFinal", 2));
                        else if (rbFinalizedNo.Checked)
                            cmd.Parameters.Add(new MySqlParameter("@isFinal", 1));

                        //checking second section (listboxes)
                        //if(lbPC.Items.Count > 0)

                        conn.Open();
                        Int32 execNonQueryResult = cmd.ExecuteNonQuery();
                        if (execNonQueryResult < 1)
                        {
                            lblAppDetailsErrors.Visible = true;
                            lblAppDetailsErrors.Text = "stored procedure error when saving application";
                        }
                        else if (execNonQueryResult > 0)
                            SavePreReq();
                    }
                    Response.Redirect("ApplicationForm.aspx");
                }//End try

                catch (MySqlException mysqlEx)
                {
                    lblAppDetailsErrors.Visible = true;
                    lblAppDetailsErrors.Text = "Error saveButtonsTopBottom: (" + mysqlEx.Message + ")";
                }
                catch (InvalidCastException invCastEx)
                {
                    lblAppDetailsErrors.Visible = true;
                    lblAppDetailsErrors.Text = "Error saveButtonsTopBottom: (" + invCastEx.Message + ")";
                }
                catch (NullReferenceException nullRefEx)
                {
                    lblAppDetailsErrors.Visible = true;
                    lblAppDetailsErrors.Text = "Error saveButtonsTopBottom: (" + nullRefEx.Message + ")";
                }
                catch (Exception ex)
                {
                    lblAppDetailsErrors.Visible = true;
                    lblAppDetailsErrors.Text = "Error saveButtonsTopBottom: General error (" + ex.Message + ")";
                }
            }
        }

        //
        //create new application must use this ID (returns MAX Id + 1)
        //
        private Int32 SetNewApplicationId()
        {
            Int32 newAppId = 0;
            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("getNewApplicationId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            newAppId = Convert.ToInt32(rdr["maxId"]);
                    }
                }
                return newAppId + 1;
            }
            catch (MySqlException mysqlEx)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "MysqlException Error (SetNewApplicationId()): " + mysqlEx.Message;
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "General Error (SetNewApplicationId()): " + Ex.Message;
            }
            return newAppId;
        }

        private List<ListItem> SortList(List<ListItem> items)
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
        private void FillListBox()
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
            foreach (ListItem item in SortList(li))
            {
                lbPreRequiredCourses.Items.Add(item);
            }
        }

        //test: bind data source MySQL to server control ListView (comments section)
        ////protected void BindDataListView()
        ////{
        ////    try                     //THIS IS USING CONNECTIONG STRING 1 (TEST DB), SP IS STORED IN THAT DB
        ////    {
        ////        ConnectionStringClass connStrings = new ConnectionStringClass();
        ////        List<Application> listOfAppObj = new List<Application>();

        ////        using (MySqlConnection conn = new MySqlConnection(connStrings.ConnStr1))
        ////        using (MySqlCommand cmd = new MySqlCommand("GetTestListView", conn))
        ////        {
        ////            cmd.CommandType = CommandType.StoredProcedure;
        ////            conn.Open();

        ////            using (MySqlDataReader rdr = cmd.ExecuteReader())
        ////            {
        ////                Application appObj;
        ////                while (rdr.Read())
        ////                {
        ////                    appObj = new Application();
        ////                    appObj.CommentId = Convert.ToInt32(rdr["id"].ToString());
        ////                    appObj.CommentBody = rdr["commentBody"].ToString();
        ////                    listOfAppObj.Add(appObj);
        ////                }
        ////            }
        ////        }
        ////        lvComment.DataSource = listOfAppObj;
        ////        lvComment.DataBind();
        ////    }
        ////    catch (NullReferenceException nullRefEx)
        ////    {
        ////        lblErrorApplicationForm.Text = "Error bindDataListView: (" + nullRefEx.Message + ")";
        ////    }
        ////    catch (MySqlException mysqlEx)
        ////    {
        ////        lblErrorApplicationForm.Text = "Error bindDataListView: (" + mysqlEx.Message + ")";
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        lblErrorApplicationForm.Text = "Error bindDataListView: General error (" + ex.Message + ")";
        ////    }
        ////}

        protected void PopulateDDLProgram()
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
                        {                                        //text(programName),    value(programId)
                            ddlProgram.Items.Add(new ListItem(rdr["Name"].ToString(), rdr["Id"].ToString()));
                        }
                    }
                }
            }
            catch (MySqlException mysqlEx)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Error bindDDLProgram: (" + mysqlEx.Message + ")";
            }
            catch (NullReferenceException nullRefEx)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Error bindDDLProgram: (" + nullRefEx.Message + ") , not connected to db";
            }
            catch (Exception ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Error bindDDLProgram: General error (" + ex.Message + ")";
            }
        }

        private void PopulateDDLTerm()
        {
            
            ddlTerm.Items.Add(new ListItem("----SELECT ONE----", "----SELECT ONE----"));
            ddlTerm.Items.Add(new ListItem("Spring", "Spring"));
            ddlTerm.Items.Add(new ListItem("Summer", "Summer"));
            ddlTerm.Items.Add(new ListItem("Fall", "Fall"));
        }

        private DateTime LastViewedDate()
        {
            //lblLastViewedDate.Text = lastReviewedDate.ToString("MM/dd/yyyy");
            return DateTime.Now;   //use today instead
        }

        private Boolean CheckIf_Atleast_OneApp_Exists()
        {
            Boolean exists = false;

            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("checkIfAtLeastOneAppExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                            exists = true;
                        else
                            exists = false;
                    }
                }
                return exists;
            }
            catch (MySqlException mysqlEx)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "MysqlException Error (Check if app exists): " + mysqlEx.Message;
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "General Exception Error (Check if app exists): " + Ex.Message;
            }
            return exists;
        }


        #endregion

        //protected void btnSubmitComment_Click(object sender, EventArgs e)
        //{
        //    //txtTestComment.Text = txtComment.Text;
        //    String currentComment = txtComment.Text; //limit to 512

        //    ConnectionStringClass connString = new ConnectionStringClass();

        //    using (MySqlConnection conn = new MySqlConnection(connString.ConnStr2))
        //    using (MySqlCommand cmd = new MySqlCommand("", conn))
        //    {
        //        //TODO: SQL query
        //        //Stored Procedure: SELECT comments based on appId
        //        //
        //    }
        //}
    }
}