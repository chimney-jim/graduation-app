<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewApplication.aspx.cs" Inherits="GraduateAppProcessing.ViewApplication" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

     <link href="Styles/SiteApplicationForm.css" rel="Stylesheet" type="text/css" />
        <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
        <script type="text/javascript">
            $().ready(function () {
                var MaxLength = 50;

                $('#<%= txtComment.ClientID %>').bind('keyup keydown blur copy paste', function (e) {
                    if ($(this).val().length > MaxLength) {
                        $(this).val($(this).val().substring(0, MaxLength));
                    }
                });
            });

            $().ready(function () {
                $('#<%= txtDateSubmitted.ClientID %>').attr('disabled', 'disabled');
                $('#<%= txtFirstName.ClientID %>').attr('disabled', 'disabled');
                $('#<%= txtLastName.ClientID %>').attr('disabled', 'disabled');
                $('#<%= ddlProgram.ClientID %>').attr('disabled', 'disabled');
                $('#<%= ddlTerm.ClientID %>').attr('disabled', 'disabled');
                $('#<%= txtYear.ClientID %>').attr('disabled', 'disabled');


                $('#<%= rbFinalizedYes.ClientID %>').click(function () {
                    if ($('#<%= rbFinalizedYes.ClientID %>').is(':checked')) {
                        $('input[type="submit"]').attr('disabled', 'disabled');
                        $('select').attr('disabled', 'disabled');
                        $('#<%= txtComment.ClientID %>').attr('disabled', 'disabled');
                        $('#<%= btnSubmitComment.ClientID %>').attr('disabled', 'disabled');
                        $('#<%= lblAppDetailsErrors.ClientID %>').hide();       //hide error label
                        $('#<%= lblErrorApplicationForm.ClientID %>').hide();
                        $('#<%= lblErrorApplicationForm.ClientID %>').hide();
                        $('#<%= rbUndecided.ClientID %>').attr('disabled', 'disabled');
                        $('#<%= rbAccepted.ClientID %>').attr('disabled', 'disabled');
                        $('#<%= rbDenied.ClientID %>').attr('disabled', 'disabled');
                        $('#<%= btnPrevious.ClientID %>').removeAttr('disabled', 'disabled');
                        $('#<%= btnNext.ClientID %>').removeAttr('disabled', 'disabled');
                        $('#<%= btnSaveEdit.ClientID %>').removeAttr('disabled', 'disabled');
                    }
                });

                $('#<%= rbFinalizedNo.ClientID %>').click(function () {
                    if ($('#<%= rbFinalizedNo.ClientID %>').is(':checked')) {
                        //$('input[type="text"]').removeAttr('disabled', 'disabled'); //attr('attributeName','attributevalue')
                        $('input[type="submit"]').removeAttr('disabled', 'disabled');
                        $('select').removeAttr('disabled', 'disabled');

                        $('#<%= ddlProgram.ClientID %>').attr('disabled', 'disabled');
                        $('#<%= ddlTerm.ClientID %>').attr('disabled', 'disabled');

                        $('textarea').removeAttr('disabled', 'disabled');
                        $('#<%= rbUndecided.ClientID %>').removeAttr('disabled', 'disabled');
                        $('#<%= rbAccepted.ClientID %>').removeAttr('disabled', 'disabled');
                        $('#<%= rbDenied.ClientID %>').removeAttr('disabled', 'disabled');
                    }
                });


                //on load check if rbFinalizedYes is checked (disable controls if true)
                //boolFlag = $('#<%= rbFinalizedYes.ClientID %>').is(':checked');
                if ($('#<%= rbFinalizedYes.ClientID %>').is(':checked')) {
                    $('input[type="text"]').attr('disabled', 'disabled');   //attr('attributeName','attributevalue')
                    $('input[type="submit"]').attr('disabled', 'disabled');
                    $('select').attr('disabled', 'disabled');
                    $('textarea').attr('disabled', 'disabled');

                    $('#<%= lblAppDetailsErrors.ClientID %>').hide();       //hide error label
                    $('#<%= lblErrorApplicationForm.ClientID %>').hide();
                    $('#<%= lblErrorApplicationForm.ClientID %>').hide();
                    $('#<%= rbUndecided.ClientID %>').attr('disabled', 'disabled');
                    $('#<%= rbAccepted.ClientID %>').attr('disabled', 'disabled');
                    $('#<%= rbDenied.ClientID %>').attr('disabled', 'disabled');
                    $('#<%= btnPrevious.ClientID %>').removeAttr('disabled', 'disabled');
                    $('#<%= btnNext.ClientID %>').removeAttr('disabled', 'disabled');
                    //$('#<%= btnSaveEdit.ClientID %>').removeAttr('disabled', 'disabled');
                }

            });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="btnPrevious" runat="server" Text="Previous" 
        onclick="btnPrevious_Click" />
    <asp:Button ID="btnNext" runat="server" Text="Next" onclick="btnNext_Click" />
    <asp:Label ID="Label1" runat="server" Text="temp label"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="btnSaveEdit" runat="server" Text="Save Edit" 
        onclick="btnSaveEdit_Click" />

    <br /><br /><br />
    <fieldset id="appDetails">
        <legend>Application Details</legend>

        <div id="leftInnerContainerAppDetails">
            <div class="leftControlsAppDetails">
                <div class="leftInnerContainerLeftControls">
                    <asp:Label ID="lblApplicationID" CssClass="leftInnerContainerLabel" runat="server" Text="Application ID: "></asp:Label>
                </div>
                <div class="leftInnerContainerRightControls">
                    <asp:Label ID="lblApplicationIdNumber" runat="server" Text="0"></asp:Label><br />
                </div>
            </div>
            <div class="leftControlsAppDetails">
                <div class="leftInnerContainerLeftControls">
                    <asp:Label ID="lblDateSubmitted" CssClass="leftInnerContainerLabel" runat="server" Text="Date Submitted: "></asp:Label>
                </div>
                <div class="leftInnerContainerRightControls">
                    <asp:TextBox ID="txtDateSubmitted" runat="server" ReadOnly="true"></asp:TextBox><br />
                </div>
            </div>
            <div class="leftControlsAppDetails">
                <div class="leftInnerContainerLeftControls">
                    <asp:Label ID="lblFirstName" CssClass="leftInnerContainerLabel" runat="server" Text="First Name: "></asp:Label>
                </div>
                <div class="leftInnerContainerRightControls">
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox><br />
                </div>
            </div>
            <div class="leftControlsAppDetails">
                <div class="leftInnerContainerLeftControls">
                    <asp:Label ID="lblLastName" CssClass="leftInnerContainerLabel" runat="server" Text="Last Name: "></asp:Label>
                </div>
                <div class="leftInnerContainerRightControls">
                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" ReadOnly="true"></asp:TextBox><br />
                </div>
            </div>
            <div class="leftControlsAppDetails">
                <div class="leftInnerContainerLeftControls">
                    <asp:Label ID="lblProgram" CssClass="leftInnerContainerLabel" runat="server" Text="Program: "></asp:Label>
                </div>
                <div class="leftInnerContainerRightControls">
                    <asp:DropDownList ID="ddlProgram" runat="server" Width="142px" 
                            AutoPostBack="True" Enabled="false"></asp:DropDownList><br />
                </div>
            </div>
            <div class="leftControlsAppDetails">
                <div class="leftInnerContainerLeftControls">
                    <asp:Label ID="lblTerm" CssClass="leftInnerContainerLabel" runat="server" Text="Term: "></asp:Label>
                </div>
                <div class="leftInnerContainerRightControls">
                    <asp:DropDownList ID="ddlTerm" runat="server" Width="142px" Enabled="false">
                    </asp:DropDownList>
                    <br />
                </div>
            </div>
            <div class="leftControlsAppDetails">
                <div class="leftInnerContainerLeftControls">
                    <asp:Label ID="lblYear" CssClass="leftInnerContainerLabel" runat="server" Text="Year: "></asp:Label>
                </div>
                <div class="leftInnerContainerRightControls">
                    <asp:TextBox ID="txtYear" runat="server" MaxLength="4" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
        </div>

        <div id="rightInnerContainerAppDetails">
            <div class="rightControlsAppDetails">
                <div class="rightInnerContainerLeftControls">
                    <asp:Label ID="lblTimesReviewed" CssClass="rightInnerContainerLabel" runat="server" Text="Times Viewed: "></asp:Label>
                </div>
                <div class="rightInnerContainerRightControls">
                    <asp:Label ID="lblTimesViewed" runat="server" Text="N/A"></asp:Label><br />
                </div>
            </div>
            
            <div class="rightControlsAppDetails">
                <div class="rightInnerContainerLeftControls">
                    <asp:Label ID="lblLastReviewed" CssClass="rightInnerContainerLabel" runat="server" Text="Last Viewed: "></asp:Label>
                </div>
                <div class="rightInnerContainerRightControls">
                    <asp:Label ID="lblLastViewed" runat="server" Text="N/A"></asp:Label><br />
                </div>
            </div>
            <div class="rightControlsAppDetails">
                <div class="rightInnerContainerLeftControls">
                    <asp:Label ID="lblIsAccepted" runat="server" CssClass="rightInnerContainerLabel" Text="Is Accepted: "></asp:Label>
                </div>
                <div class="rightInnerContainerRadioButtons">
                    <asp:RadioButton ID="rbUndecided" runat="server" Text="Undecided" GroupName="groupIsAccepted"/>
                </div>
            </div>
            <div class="rightControlsAppDetails">
                <div class="rightInnerContainerLeftControls">
                    <asp:Label ID="Label1Hidden" runat="server" CssClass="rightInnerContainerLabel" Text="" Visible="False"></asp:Label>
                </div>
                <div class="rightInnerContainerRadioButtons">
                    <asp:RadioButton ID="rbAccepted" runat="server" Text="Accepted" GroupName="groupIsAccepted"/>
                </div>
            </div>
            <div class="rightControlsAppDetails">
                <div class="rightInnerContainerLeftControls">
                    <asp:Label ID="Label2Hidden" runat="server" CssClass="rightInnerContainerLabel" Text="" Visible="False"></asp:Label>
                </div>
                <div class="rightInnerContainerRadioButtons">
                    <asp:RadioButton ID="rbDenied" runat="server" Text="Denied" GroupName="groupIsAccepted"/>
                </div>
            </div>
            <div class="rightControlsAppDetails">
                <div class="rightInnerContainerLeftControls">
                    <asp:Label ID="Label3Hidden" CssClass="rightInnerContainerLabel" runat="server" Text="" Visible="False"></asp:Label>
                </div>
                <div class="rightInnerContainerRadioButtons">
                    <asp:Label ID="Label4Hidden" runat="server" CssClass="rightInnerContainerLabel" Text="" Visible="False"></asp:Label>
                </div>
            </div>
            <div class="rightControlsAppDetails">
                <div class="rightInnerContainerLeftControls">
                    <asp:Label ID="lblFinalized" CssClass="rightInnerContainerLabel" runat="server" Text="FINALIZED: "></asp:Label>
                </div>
                <div class="rightInnerContainerRadioButtons">
                    <asp:RadioButton ID="rbFinalizedYes" runat="server" Text="YES" GroupName="groupFinalized" />
                    <asp:RadioButton ID="rbFinalizedNo" runat="server" Text="NO" GroupName="groupFinalized" />
                </div>
            </div>
        </div>
    </fieldset>
    <div>
        <asp:Label ID="lblAppDetailsErrors" CssClass="ErrorLabels" runat="server" Text=""></asp:Label>
    </div>
    
    <fieldset id="prereqCourses">
        <legend>Pre-requisite courses</legend>
        <div id="innerContainerPreReqCourses">
            <div id="listboxPreRequiredCourses">
                <asp:ListBox ID="lbPreRequiredCourses" runat="server" SelectionMode="Single" Height="200px" Width="200px" />
            </div>
            <div id="moveButtons">
                <div id="buttonMovePC">
                    <asp:Button ID="btnMovePC" CssClass="buttonPCPTRQNNNE" runat="server" Text="PC" onclick="btnMovePC_Click" />
                </div>
                <div id="buttonMovePT">
                    <asp:Button ID="btnMovePT" CssClass="buttonPCPTRQNNNE" runat="server" Text="PT" 
                        onclick="btnMovePT_Click" />
                </div>
                <div id="buttonMoveRQ">
                    <asp:Button ID="btnMoveRQ" CssClass="buttonPCPTRQNNNE" runat="server" Text="RQ" 
                        onclick="btnMoveRQ_Click" />
                </div>
                <div id="buttonMoveNN">
                    <asp:Button ID="btnMoveNN" CssClass="buttonPCPTRQNNNE" runat="server" Text="NN" 
                        onclick="btnMoveNN_Click" />
                </div>
                <div id="buttonMoveRE">
                    <asp:Button ID="btnMoveRE" CssClass="buttonPCPTRQNNNE" runat="server" Text="RE" 
                        onclick="btnMoveRE_Click" />
                </div>
            </div>
            <div id="lbbtnPC">
                <asp:ListBox ID="lbPC" runat="server" Width="150px"></asp:ListBox>
                <div id="buttonClearPC">
                    <asp:Button ID="btnClearPC" runat="server" Text="Clear PC" onclick="btnClearPC_Click"  Width="151px" />
                </div>
            </div>
            <div id="lbbtnPT">
                <asp:ListBox ID="lbPT" runat="server" Width="150px"></asp:ListBox>
                <div id="buttonClearPT">
                    <asp:Button ID="btnClearPT" runat="server" Text="Clear PT" Width="151px" 
                        onclick="btnClearPT_Click" />
                </div>
            </div>
            <div id="lbbtnRQ">
                <asp:ListBox ID="lbRQ" runat="server" Width="150px"></asp:ListBox>
                <div id="buttonClearRQ">
                    <asp:Button ID="btnClearRQ" runat="server" Text="Clear RQ" Width="151px" 
                        onclick="btnClearRQ_Click" />
                </div>
            </div>
            <div id="lbbtnNNandRE">
                <div id="lbbtnNN">
                    <asp:ListBox ID="lbNN" runat="server" Width="150px"></asp:ListBox>
                    <div id="buttonClearNN">
                        <asp:Button ID="btnClearNN" runat="server" Text="Clear NN" Width="151px" 
                            onclick="btnClearNN_Click" />
                    </div>
                </div>
                <div id="lbbtnRE">
                    <asp:ListBox ID="lbRE" runat="server" Width="150px"></asp:ListBox>
                    <div id="buttonClearRE">
                        <asp:Button ID="btnClearRE" runat="server" Text="Clear RE" Width="151px" 
                            onclick="btnClearRE_Click" />
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
    <div>
        <asp:Label ID="lblPreReqErrors" CssClass="ErrorLabels" runat="server" Text=""></asp:Label>
    </div>
    
    <fieldset id="comment">
        <legend>Comments</legend>
        <asp:Label ID="lblTempComment" runat="server" Text="Placeholder: This is where the comments will be shown"></asp:Label>
        <br /><br />
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmitComment" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:ListView ID="lvComment" runat="server">
                    <ItemTemplate>
                        <li class="commentItem">
                            <asp:Label ID="lblId" Text='<%#Eval("CommentId") %>' runat="server" Width="20px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCommentBody" Text='<%#Eval("CommentBody") %>' runat="server"></asp:Label>
                        </li>
                        <hr />
                    </ItemTemplate>
                </asp:ListView>
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                Comment (textbox/area):
                <asp:TextBox ID="txtComment" CssClass="textAreaComments" runat="server" Width="500px" TextMode="MultiLine"></asp:TextBox><br />
                <asp:Button ID="btnSubmitComment" runat="server" Text="Submit Comment" 
                    onclick="btnSubmitComment_Click" /><br />
                <asp:Label ID="lblErrorApplicationForm" runat="server" ForeColor="#FF3300"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <br /><br />
</asp:Content>
