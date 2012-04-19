using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GraduateAppProcessing
{
    public partial class ViewApplication : System.Web.UI.Page
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
                lblAppDetailsErrors.Text = String.Empty;
                lblAppDetailsErrors.Visible = false;
                lblPreReqErrors.Text = String.Empty;
                lblPreReqErrors.Visible = false;
                lblErrorApplicationForm.Text = String.Empty;
                lblErrorApplicationForm.Visible = false;
                txtComment.Text = String.Empty;

                PopulateDDLProgram();
                PopulateDDLTerm();

                if(String.IsNullOrEmpty(Request.QueryString["AppId"]))                   
                {
                    if(this.Session["AppIdQueryString"] == null)
                    {
                        if (CheckIf_Atleast_OneApp_Exists())
                        {
                            //lblApplicationIdNumber.Text = "1";
                            lblApplicationIdNumber.Text = GetMinApplicationId().ToString();
                            LoadAppDetails(GetMinApplicationId());
                            LoadComments();
                            SaveLastViewed();
                        }
                        else    //if no app exists
                        { Response.Redirect("Error_ViewApplication.aspx"); }
                    }
                    else    //if Session["AppIdQueryString] is not null or empty
                    {
                        String appId = this.Session["AppIdQueryString"].ToString();
                        //Request.QueryString["AppId"] = "";
                        //String url = HttpContext.Current.Request.Url.AbsoluteUri;
                        //Uri newURI = new Uri(url);
                        //newURI.GetLeftPart(UriPartial.Path);
                        //
                    
                        lblApplicationIdNumber.Text = appId;
                        LoadAppDetails(Convert.ToInt32(lblApplicationIdNumber.Text));
                        LoadComments();
                        SaveLastViewed();
                    }
                }
                else    //if QueryString["AppId"] is not null or empty
                {
                    this.Session["AppIdQueryString"] = Request.QueryString["AppId"];
                    Response.Redirect(Request.Path);
                }
                /*If session["appId"] isset then
                 * {
                 *      pull up application based on appId
                 * }
                 * else if_not_set
                 * {
                 *      if(check if at least one exists)
                 *      { pull up app based on GetMinApplicationId() }
                 * }
                 * */
                
            }
            else //if postback happens
            {
                //lblApplicationIdNumber.Text = Request.QueryString["appNumber"];
                //loadAppDetails(Convert.ToInt32(lblApplicationIdNumber.Text));
            }

            if (CheckIf_Atleast_OneApp_Exists())
                SaveTimesViewed();
        }
        
        private void SaveTimesViewed()
        {
            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("setTimesReviewed", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "SaveTimesViewed Error: " + Ex.Message;
            }
        }

        private void LoadTimesViewed()
        {

        }


        //LastViewed date, call to previous / next:
        //Load the current date from DB first
        //Then save DateTime.Now / Today to replace current date in DB
        private void SaveLastViewed()
        {
            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("setLastViewed", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "LoadLastViewed Error: " + Ex.Message;
            }
        }

        private void LoadLastViewed()
        {
            //lblLastViewedDate.Text = lastReviewedDate.ToString("MM/dd/yyyy");

            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("loadLastViewed", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));

                    conn.Open();
                    String lastViewedDate = String.Empty;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {//String dateSubmitted = DateTime.Parse(rdr["DateInputed"].ToString()).ToString("MM-dd-yyyy");
                            lastViewedDate = DateTime.Parse(rdr["lastReviewed"].ToString()).ToString("MM-dd-yyyy HH:mm:ss");
                            lblLastViewed.Text = lastViewedDate;
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "LoadLastViewed Error: " + Ex.Message;
            }
        }

        

        

        

        //test: bind data source MySQL to server control ListView (comments section)
        protected void BindDataListView()
        {
            try                     //THIS IS USING CONNECTIONG STRING 1 (TEST DB), SP IS STORED IN THAT DB
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

        

        


        #region EVENTS

        ////protected void btnSaveBottom_Click(object sender, EventArgs e)
        ////{
        ////    saveButtonsTopBottom();
        ////}

        ////protected void btnSaveTop_Click(object sender, EventArgs e)
        ////{
        ////    saveButtonsTopBottom();
        ////}

        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtComment.Text))
            {
                lblErrorApplicationForm.Visible = true;
                lblErrorApplicationForm.Text = "Cannot submit empty comment.";
            }
            else
            {
                SubmitComment();
                LoadComments();                 //Need to rebind to datasource then updatepanel (AJAX)
                UpdatePanel1.Update();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            lblAppDetailsErrors.Text = String.Empty;
            lblAppDetailsErrors.Visible = false;
            lblPreReqErrors.Text = String.Empty;
            lblPreReqErrors.Visible = false;
            lblErrorApplicationForm.Text = String.Empty;
            lblErrorApplicationForm.Visible = false;

            Int32 appNumber = 0;

            if (String.IsNullOrEmpty(lblApplicationIdNumber.Text))
                appNumber = 1;
            else
                appNumber = Convert.ToInt32(lblApplicationIdNumber.Text) + 1;

            if (appNumber >= (GetMaxApplicationId() + 1)) { }
            else
            {
                lblApplicationIdNumber.Text = appNumber.ToString();
                LoadAppDetails(Convert.ToInt32(lblApplicationIdNumber.Text));
                LoadComments();
                SaveLastViewed();
            }
            Re_populateThreeRadioButtons();
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            lblAppDetailsErrors.Text = String.Empty;
            lblAppDetailsErrors.Visible = false;
            lblPreReqErrors.Text = String.Empty;
            lblPreReqErrors.Visible = false;
            lblErrorApplicationForm.Text = String.Empty;
            lblErrorApplicationForm.Visible = false;

            if (String.IsNullOrEmpty(lblApplicationIdNumber.Text))
            { }
            else
            {
                Int32 appNumber = Convert.ToInt32(lblApplicationIdNumber.Text);
                if (appNumber < 1) { }
                else if (appNumber > 0)
                {
                    appNumber -= 1;
                    if (appNumber == 0) { }         //{  } = do nothing
                    else
                    {
                        lblApplicationIdNumber.Text = appNumber.ToString();
                        LoadAppDetails(Convert.ToInt32(lblApplicationIdNumber.Text));
                        LoadComments();
                        SaveLastViewed();
                    }
                }
            }
            Re_populateThreeRadioButtons();
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
            catch (NullReferenceException nullRefEx)
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

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            if (lbPC.Items.Count > 0 || lbPT.Items.Count > 0 ||
                lbRQ.Items.Count > 0 || lbNN.Items.Count > 0 || lbRE.Items.Count > 0)
            {
                lblAppDetailsErrors.Text = String.Empty;
                lblAppDetailsErrors.Visible = false;

                ClearListsBoxes();
                if (SavePreReq())
                {
                    UpdateAppIsFinal();
                    Re_populateThreeRadioButtons();     //undecided, denied, accepted
                }
            }
            else
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "PC, PT, RQ, NN, RE listboxes cannot all be empty";
            }
            
        }


        ////protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        ////{
        ////    //Clear all 5 listboxes (PC, PT, RQ, NN, RE)
        ////    lbPC.Items.Clear();
        ////    lbPT.Items.Clear();
        ////    lbRQ.Items.Clear();
        ////    lbNN.Items.Clear();
        ////    lbRE.Items.Clear();

        ////    if (ddlProgram.SelectedItem.Text.Contains("SELECT ONE"))
        ////    { lbPreRequiredCourses.Items.Clear(); }

        ////    else if (!ddlProgram.SelectedItem.Text.Contains("SELECT ONE"))
        ////    {
        ////        try
        ////        {
        ////            lbPreRequiredCourses.Items.Clear();
        ////            ConnectionStringClass connString = new ConnectionStringClass();

        ////            using (MySqlConnection conn = new MySqlConnection(connString.ConnStr2))
        ////            using (MySqlCommand cmd = new MySqlCommand("getPrerequisiteCourses", conn))
        ////            {
        ////                cmd.CommandType = CommandType.StoredProcedure;
        ////                cmd.Parameters.Add(new MySqlParameter("@progid", ddlProgram.SelectedValue));
        ////                conn.Open();

        ////                using (MySqlDataReader rdr = cmd.ExecuteReader())
        ////                {
        ////                    //List<String> listOfString = new List<String>();
        ////                    while (rdr.Read())
        ////                    {
        ////                        lbPreRequiredCourses.Items.Add(new ListItem(rdr["pre-requisite"].ToString(),
        ////                                                                    rdr["Id"].ToString()));
        ////                    }
        ////                }
        ////                //txtFirstName.Text = ddlProgram.SelectedValue;
        ////            }
        ////        }
        ////        catch (MySqlException mysqlEx)
        ////        {
        ////            lblErrorApplicationForm.Text = "Error ddlProgram_SelectedIndexChanged: (" + mysqlEx.Message + ")";
        ////        }
        ////        catch (NullReferenceException nullRefEx)
        ////        {
        ////            lblErrorApplicationForm.Text = "Error ddlProgram_SelectedIndexChanged: (" + nullRefEx.Message + ")";
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            lblErrorApplicationForm.Text = "Error ddlProgram_SelectedIndexChanged: General error (" + ex.Message + ")";
        ////        }
        ////    }//END ELSE IF
        ////}

        


        #endregion

        #region MISC

        private void SubmitComment()
        {
            lblErrorApplicationForm.Visible = false;
            lblErrorApplicationForm.Text = String.Empty;

            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("submitComment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));
                    cmd.Parameters.Add(new MySqlParameter("@commentBody", txtComment.Text));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    txtComment.Text = String.Empty;
                }
            }
            catch (Exception Ex)
            {
                lblErrorApplicationForm.Visible = true;
                lblErrorApplicationForm.Text = "SubmitComment Error: " + Ex.Message;
            }
        }
        
        private void LoadComments()
        {
            List<Application> listOfAppObjs = new List<Application>();
            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("loadComments", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));

                    conn.Open();
                    Application appObj;
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                appObj = new Application();
                                appObj.CommentId = Convert.ToInt32(rdr["Id"]);
                                appObj.CommentBody = rdr["Body"].ToString();
                                listOfAppObjs.Add(appObj);
                            }
                        }
                        else
                        {
                            lblTempComment.Text = "No comment exists for this application.";
                        }
                    }
                }
                lvComment.DataSource = listOfAppObjs;
                lvComment.DataBind();
            }
            catch (Exception Ex)
            {
                lblErrorApplicationForm.Visible = true;
                lblErrorApplicationForm.Text = "LoadComments Error: " + Ex.Message;
            }
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

        private void Re_populateThreeRadioButtons()
        {
            rbUndecided.Checked = false;
            rbDenied.Checked = false;
            rbAccepted.Checked = false;
            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("re_populateThreeRadioButtons", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));

                    conn.Open();

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (Convert.ToInt32(rdr["IsAccepted"]).Equals(1))
                            {
                                rbUndecided.Checked = false;
                                rbDenied.Checked = true;            //*
                                rbAccepted.Checked = false;
                            }
                            if (Convert.ToInt32(rdr["IsAccepted"]).Equals(2))
                            {
                                rbUndecided.Checked = false;
                                rbDenied.Checked = false;
                                rbAccepted.Checked = true;          //*
                            }
                            if (rdr["isAccepted"].Equals(DBNull.Value) || (rdr["isAccepted"]).ToString().Equals("0"))
                            {
                                rbUndecided.Checked = true;         //*
                                rbDenied.Checked = false;
                                rbAccepted.Checked = false;
                            }
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Re_populateThreeRadioButtons Error: " + Ex.Message;
            }
        }

        private void LoadDefaultPreReqCourses(Int32 progId, List<ListItem> myItems)
        {
            lbPreRequiredCourses.Items.Clear();

            List<ListItem> allItems = new List<ListItem>();
            ConnectionStringClass connStr = new ConnectionStringClass();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("getPrerequisiteCourses", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@progid", progId));

                    conn.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        ListItem myItem = new ListItem();
                        while (rdr.Read())
                        {
                            allItems.Add(new ListItem(rdr["pre-requisite"].ToString(), rdr["Id"].ToString()));
                        }
                    }
                }

                for (int i = 0; i < allItems.Count - 1; i++)        //remove common items
                {
                    foreach (ListItem item in myItems)
                    {
                        if (allItems.Contains(item))
                        {
                            allItems.Remove(item);
                        }
                    }
                }
                foreach (ListItem item in allItems)                 //populat lbPreRequisiteCourses with listitems
                    lbPreRequiredCourses.Items.Add(item);
            }
            catch
            { throw; }
        }

        private void LoadAppPreReq(Int32 currAppId, Int32 currProgId)
        {
            lbPC.Items.Clear();
            lbPT.Items.Clear();
            lbNN.Items.Clear();
            lbRE.Items.Clear();
            lbRQ.Items.Clear();

            ConnectionStringClass connStr = new ConnectionStringClass();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("loadAppPreReq", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", currAppId));

                    conn.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        //rdr returns appPReq.PreReqId, pReq.Name, appPReq.PreReqStatusId
                        List<ListItem> myItems = new List<ListItem>();
                        while (rdr.Read())
                        {
                            TextBox1.Text = "loadappprereq atm";
                            myItems.Add(new ListItem(rdr["Name"].ToString(), rdr["PreReqId"].ToString()));
                            if (Convert.ToInt32(rdr["PreReqStatusId"].ToString()).Equals(1))
                            {
                                lbPC.Items.Add(new ListItem(rdr["Name"].ToString(), rdr["PreReqId"].ToString()));
                            }
                            else if (Convert.ToInt32(rdr["PreReqStatusId"].ToString()).Equals(2))
                            {
                                lbPT.Items.Add(new ListItem(rdr["Name"].ToString(), rdr["PreReqId"].ToString()));
                            }
                            else if (Convert.ToInt32(rdr["PreReqStatusId"].ToString()).Equals(3))
                            {
                                lbRQ.Items.Add(new ListItem(rdr["Name"].ToString(), rdr["PreReqId"].ToString()));
                            }
                            else if (Convert.ToInt32(rdr["PreReqStatusId"].ToString()).Equals(4))
                            {
                                lbNN.Items.Add(new ListItem(rdr["Name"].ToString(), rdr["PreReqId"].ToString()));
                            }
                            else if (Convert.ToInt32(rdr["PreReqStatusId"].ToString()).Equals(5))
                            {
                                lbRE.Items.Add(new ListItem(rdr["Name"].ToString(), rdr["PreReqId"].ToString()));
                            }
                        }
                        LoadDefaultPreReqCourses(currProgId, myItems);      //pass current selected prereqs to other method
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void LoadAppDetails(Int32 currentAppId)
        {
            ConnectionStringClass connStr = new ConnectionStringClass();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("loadApplication", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", currentAppId));
                    conn.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                String dateSubmitted = DateTime.Parse(rdr["DateInputed"].ToString()).ToString("MM-dd-yyyy");
                                txtDateSubmitted.Text = dateSubmitted;
                                txtFirstName.Text = rdr["StudentFirstName"].ToString();
                                txtLastName.Text = rdr["StudentLastName"].ToString();
                                ddlProgram.SelectedValue = rdr["ProgramId"].ToString();
                                ddlTerm.SelectedValue = rdr["AppTerm"].ToString();
                                txtYear.Text = rdr["AppYear"].ToString();


                                if (Convert.ToInt32(rdr["IsAccepted"]).Equals(1))
                                {
                                    rbUndecided.Checked = false;
                                    rbDenied.Checked = true;            //*
                                    rbAccepted.Checked = false;
                                }
                                if (Convert.ToInt32(rdr["IsAccepted"]).Equals(2))
                                {
                                    rbUndecided.Checked = false;
                                    rbDenied.Checked = false;
                                    rbAccepted.Checked = true;          //*
                                }
                                if (rdr["isAccepted"].Equals(DBNull.Value) || (rdr["isAccepted"]).ToString().Equals("0"))
                                {
                                    rbUndecided.Checked = true;         //*
                                    rbDenied.Checked = false;
                                    rbAccepted.Checked = false;
                                }


                                if (rdr["isFinalized"] == DBNull.Value || Convert.ToInt32(rdr["isFinalized"]).Equals(0))
                                {
                                    rbFinalizedNo.Checked = false;
                                    rbFinalizedYes.Checked = false;
                                }
                                if (Convert.ToInt32(rdr["IsFinalized"]).Equals(1))
                                {
                                    rbFinalizedNo.Checked = true;       //*
                                    rbFinalizedYes.Checked = false;
                                }
                                if (Convert.ToInt32(rdr["IsFinalized"]).Equals(2))
                                {
                                    rbFinalizedNo.Checked = false;
                                    rbFinalizedYes.Checked = true;      //*
                                }
                            }
                            LoadLastViewed();
                            LoadAppPreReq(currentAppId, Convert.ToInt32(ddlProgram.SelectedValue));
                            //
                            //LOAD COMMENT HERE
                            //
                        }//end if

                    }
                }
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Text = "Exception Error in loadAppDetails: " + Ex.Message;
            }
        }
        
        private Int32 GetMaxApplicationId()
        {
            Int32 maxId = 0;

            ConnectionStringClass connStr = new ConnectionStringClass();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("getMaxApplicationId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            maxId = Convert.ToInt32(rdr["maxId"]);
                    }
                }
            }
            catch (NullReferenceException nullRefEx)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Null Reference error: " + nullRefEx.Message;
            }
            catch (MySqlException mysqlEx)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "MySQLException error: " + mysqlEx.Message;
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "General Exception error: " + Ex.Message;
            }
            return maxId;
        }

        private Int32 GetMinApplicationId()
        {
            Int32 minId = 0;
            ConnectionStringClass connStr = new ConnectionStringClass();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("getMinApplicationId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                            minId = Convert.ToInt32(rdr["minId"]);
                    }
                }
            }
            catch (NullReferenceException nullRefEx)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "Null Reference error: " + nullRefEx.Message;
            }
            catch (MySqlException mysqlEx)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "MySQLException error: " + mysqlEx.Message;
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "General Exception error: " + Ex.Message;
            }
            return minId;
        }

        private void UpdateAppIsFinal()
        {
            try
            {
                ConnectionStringClass connStr = new ConnectionStringClass();

                using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                using (MySqlCommand cmd = new MySqlCommand("updateAppIsFinal", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));
                    
                    if (rbUndecided.Checked)
                        cmd.Parameters.Add(new MySqlParameter("@isAccept", "0"));    //integer 0 is considered null in mysql
                    if (rbAccepted.Checked)
                        cmd.Parameters.Add(new MySqlParameter("@isAccept", 2));
                    if (rbDenied.Checked)
                        cmd.Parameters.Add(new MySqlParameter("@isAccept", 1));
                    
                    if (rbFinalizedYes.Checked)
                        cmd.Parameters.Add(new MySqlParameter("@isFinal", 2));
                    if (rbFinalizedNo.Checked)
                        cmd.Parameters.Add(new MySqlParameter("@isFinal", 1));                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                lblAppDetailsErrors.Visible = true;
                lblAppDetailsErrors.Text = "UpdateAppIsFinal Error: " + Ex.Message;
            }
        }

        private void ClearListsBoxes()
        {
            try
            {
                if (lbPC.Items.Count > 0 || lbPT.Items.Count > 0 ||
                    lbRQ.Items.Count > 0 || lbNN.Items.Count > 0 || lbRE.Items.Count > 0)
                {
                    ConnectionStringClass connStr = new ConnectionStringClass();

                    using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
                    using (MySqlCommand cmd = new MySqlCommand("clearListBoxes", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("@appId", Convert.ToInt32(lblApplicationIdNumber.Text)));

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Ex)
            {
                lblPreReqErrors.Visible = true;
                lblPreReqErrors.Text = "ClearListBoxes error: " + Ex.Message;
            }
        }

        private Boolean SavePreReq()
        {
            Boolean saved = false;
            Int32 rowCount = 0;
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
                            rowCount = cmd.ExecuteNonQuery();
                            if (rowCount < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                            else
                                saved = true;
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
                            rowCount = cmd.ExecuteNonQuery();
                            if (rowCount < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                            else
                                saved = true;
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
                            rowCount = cmd.ExecuteNonQuery();
                            if (rowCount < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                            else
                                saved = true;
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
                            rowCount = cmd.ExecuteNonQuery();
                            if (rowCount < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                            else
                                saved = true;
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
                            rowCount = cmd.ExecuteNonQuery();
                            if (rowCount < 1)
                            {
                                lblPreReqErrors.Visible = true;
                                lblPreReqErrors.Text = "stored procedure error when saving pre-requisite PC";
                            }
                            else
                                saved = true;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                lblPreReqErrors.Visible = true;
                lblPreReqErrors.Text = "SavePreReq Error: " + Ex.Message;
            }

            return saved;
        }

        ////private void saveButtonsTopBottom()
        ////{
        ////    //txtFirstName.Text = DateTime.Today.ToString("yyyy-MM-dd");

        ////    //datetime.tryparse() -> returns true if parse is successful. False otherwise
        ////    DateTime dt;
        ////    if (String.IsNullOrEmpty(txtDateSubmitted.Text) ||
        ////             String.IsNullOrEmpty(txtFirstName.Text) ||
        ////             String.IsNullOrEmpty(txtLastName.Text) ||
        ////             String.IsNullOrEmpty(txtYear.Text))
        ////    {
        ////        lblAppDetailsErrors.Visible = true;
        ////        lblAppDetailsErrors.Text = "Date submitted, first and last name, and year are required fields";
        ////    }

        ////    else if (!DateTime.TryParse(txtDateSubmitted.Text, out dt))
        ////    {
        ////        lblAppDetailsErrors.Visible = true;
        ////        lblAppDetailsErrors.Text = "The correct date format is mm-dd-yyyy";
        ////    }

        ////    else if (!Regex.IsMatch(txtFirstName.Text, "[-'a-zA-Z]"))
        ////    {
        ////        lblAppDetailsErrors.Visible = true;
        ////        lblAppDetailsErrors.Text = "First Name can only be letters";
        ////    }

        ////    else if (!Regex.IsMatch(txtLastName.Text, "[-'a-zA-Z]"))
        ////    {
        ////        lblAppDetailsErrors.Visible = true;
        ////        lblAppDetailsErrors.Text = "Last name can only be letters";
        ////    }

        ////    else if (!Regex.IsMatch(txtYear.Text, "^[0-9]+$"))
        ////    {
        ////        lblAppDetailsErrors.Visible = true;
        ////        lblAppDetailsErrors.Text = "Year has to be numbers";
        ////    }

        ////    else if (lbPC.Items.Count < 1 &&
        ////             lbPT.Items.Count < 1 &&
        ////             lbRQ.Items.Count < 1 &&
        ////             lbNN.Items.Count < 1 &&
        ////             lbRE.Items.Count < 1)
        ////    {
        ////        lblPreReqErrors.Visible = true;
        ////        lblPreReqErrors.Text = "Prerequisite boxes cannot all be empty";
        ////    }

        ////    else
        ////    {
        ////        lblAppDetailsErrors.Text = String.Empty;
        ////        lblAppDetailsErrors.Visible = false;
        ////        lblPreReqErrors.Text = String.Empty;
        ////        lblPreReqErrors.Visible = false;

        ////        DateTime lastReviewedDate = lastViewedDate();

        ////        try
        ////        {
        ////            ConnectionStringClass connString = new ConnectionStringClass();

        ////            using (MySqlConnection conn = new MySqlConnection(connString.ConnStr2))
        ////            using (MySqlCommand cmd = new MySqlCommand("setApplication", conn))     //change SP to 'saveApplication'?
        ////            {
        ////                cmd.CommandType = CommandType.StoredProcedure;
        ////                cmd.Parameters.Add(new MySqlParameter("@firstN", txtFirstName.Text));
        ////                cmd.Parameters.Add(new MySqlParameter("@lastN", txtLastName.Text));
        ////                cmd.Parameters.Add(new MySqlParameter("@progId", ddlProgram.SelectedValue));
        ////                cmd.Parameters.Add(new MySqlParameter("@term", ddlTerm.SelectedValue));
        ////                cmd.Parameters.Add(new MySqlParameter("@year", Convert.ToInt32(txtYear.Text)));
        ////                //convert dateSubmitted to 'yyyy/dd/MM' format
        ////                cmd.Parameters.Add(new MySqlParameter("@dateInput", DateTime.Parse(txtDateSubmitted.Text).ToString("yyyy-MM-dd")));
        ////                cmd.Parameters.Add(new MySqlParameter("@app_Date", DateTime.Today.ToString("yyyy-MM-dd")));
        ////                cmd.Parameters.Add(new MySqlParameter("@lastViewed", DateTime.Today.ToString("yyyy-MM-dd")));
        ////                if (rbUndecided.Checked)
        ////                    cmd.Parameters.Add(new MySqlParameter("@isAccept", DBNull.Value));
        ////                else if (rbAccepted.Checked)
        ////                    cmd.Parameters.Add(new MySqlParameter("@isAccept", 1));
        ////                else if (rbDenied.Checked)
        ////                    cmd.Parameters.Add(new MySqlParameter("@isAccept", "0"));
        ////                if (rbFinalizedYes.Checked)
        ////                    cmd.Parameters.Add(new MySqlParameter("@isFinal", 1));
        ////                else if (rbFinalizedNo.Checked)
        ////                    cmd.Parameters.Add(new MySqlParameter("@isFinal", "0"));

        ////                //checking second section (listboxes)
        ////                //if(lbPC.Items.Count > 0)

        ////                conn.Open();
        ////                Int32 execNonQueryResult = cmd.ExecuteNonQuery();
        ////                if (execNonQueryResult < 1)
        ////                {
        ////                    lblAppDetailsErrors.Visible = true;
        ////                    lblAppDetailsErrors.Text = "stored procedure error when saving application";
        ////                }
        ////                else if (execNonQueryResult > 0)
        ////                    savePreReq();
        ////            }
        ////        }//End try

        ////        catch (MySqlException mysqlEx)
        ////        {
        ////            lblAppDetailsErrors.Visible = true;
        ////            lblAppDetailsErrors.Text = "Error saveButtonsTopBottom: (" + mysqlEx.Message + ")";
        ////        }
        ////        catch (InvalidCastException invCastEx)
        ////        {
        ////            lblAppDetailsErrors.Visible = true;
        ////            lblAppDetailsErrors.Text = "Error saveButtonsTopBottom: (" + invCastEx.Message + ")";
        ////        }
        ////        catch (NullReferenceException nullRefEx)
        ////        {
        ////            lblAppDetailsErrors.Visible = true;
        ////            lblAppDetailsErrors.Text = "Error saveButtonsTopBottom: (" + nullRefEx.Message + ")";
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            lblAppDetailsErrors.Visible = true;
        ////            lblAppDetailsErrors.Text = "Error saveButtonsTopBottom: General error (" + ex.Message + ")";
        ////        }
        ////    }
        ////}

        //
        //create new application must use this ID (returns MAX Id + 1)
        //
        ////private Int32 getNewApplicationId()
        ////{
        ////    Int32 newAppId = 0;
        ////    ConnectionStringClass connStr = new ConnectionStringClass();

        ////    using (MySqlConnection conn = new MySqlConnection(connStr.ConnStr2))
        ////    using (MySqlCommand cmd = new MySqlCommand("getNewApplicationId", conn))
        ////    {
        ////        cmd.CommandType = CommandType.StoredProcedure;

        ////        conn.Open();
        ////        using (MySqlDataReader rdr = cmd.ExecuteReader())
        ////        {
        ////            while (rdr.Read())
        ////                newAppId = Convert.ToInt32(rdr["maxId"]);
        ////        }
        ////    }
        ////    return newAppId + 1;
        ////}

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
        //private void fillListBox()
        //{
        //    lbPreRequiredCourses.Items.Clear();
        //    ListItem[] TransitioncourseList = new ListItem[10];
        //    TransitioncourseList[0] = new ListItem("Sample course 1", "sample");
        //    TransitioncourseList[1] = new ListItem("course 2", "aloha");
        //    TransitioncourseList[2] = new ListItem("Sample course 3");
        //    TransitioncourseList[3] = new ListItem("Sample course 4");
        //    TransitioncourseList[4] = new ListItem("Sample course 5a");
        //    TransitioncourseList[5] = new ListItem("Sample course 6");
        //    TransitioncourseList[6] = new ListItem("Sample course 7");
        //    TransitioncourseList[7] = new ListItem("Sample course 8");
        //    TransitioncourseList[8] = new ListItem("Sample course 9");
        //    TransitioncourseList[9] = new ListItem("Sample course 10");
        //    //TODO~~~~~~

        //    List<ListItem> li = new List<ListItem>(TransitioncourseList);
        //    //add SORTED items into listbox one by one
        //    foreach (ListItem item in sortList(li))
        //    {
        //        lbPreRequiredCourses.Items.Add(item);
        //    }
        //}

        

        ////protected void populateDDLProgram()
        ////{
        ////    ddlProgram.Items.Insert(0, "----SELECT ONE----");
        ////    try
        ////    {
        ////        ConnectionStringClass connStrings = new ConnectionStringClass();

        ////        using (MySqlConnection conn = new MySqlConnection(connStrings.ConnStr2))
        ////        using (MySqlCommand cmd = new MySqlCommand("getDDLProgram", conn))
        ////        {
        ////            cmd.CommandType = CommandType.StoredProcedure;
        ////            conn.Open();

        ////            using (MySqlDataReader rdr = cmd.ExecuteReader())
        ////            {
        ////                while (rdr.Read())
        ////                {                                        //text(programName),    value(programId)
        ////                    ddlProgram.Items.Add(new ListItem(rdr["Name"].ToString(), rdr["Id"].ToString()));
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (MySqlException mysqlEx)
        ////    {
        ////        lblErrorApplicationForm.Text = "Error bindDDLProgram: (" + mysqlEx.Message + ")";
        ////    }
        ////    catch (NullReferenceException nullRefEx)
        ////    {
        ////        lblErrorApplicationForm.Text = "Error bindDDLProgram: (" + nullRefEx.Message + ") , not connected to db";
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        lblErrorApplicationForm.Text = "Error bindDDLProgram: General error (" + ex.Message + ")";
        ////    }
        ////}

        private void PopulateDDLProgram()
        {
            ddlProgram.Items.Add(new ListItem("SWE", "1"));
            ddlProgram.Items.Add(new ListItem("CSE", "2"));
            ddlProgram.Items.Add(new ListItem("IT", "3"));
            ddlProgram.Items.Add(new ListItem("CGDD", "4"));
        }

        private void PopulateDDLTerm()
        {
            ddlTerm.Items.Add(new ListItem("Spring", "Spring"));
            ddlTerm.Items.Add(new ListItem("Summer", "Summer"));
            ddlTerm.Items.Add(new ListItem("Fall", "Fall"));
        }

        #endregion
    }
}