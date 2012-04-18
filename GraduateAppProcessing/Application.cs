using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduateAppProcessing
{
    //
    //Application Object
    //
    public class Application
    {

        #region Application section
        
        private String _AppId;
        public String AppId
        {
            get { return _AppId; }
            set { _AppId = value; }
        }

        private String _StudentFirstName;
        public String StudentFirstName
        {
            get { return _StudentFirstName; }
            set { _StudentFirstName = value; }
        }

        private String _StudentLastName;
        public String StudentLastName
        {
            get { return _StudentLastName; }
            set { _StudentLastName = value; }
        }

        private Int32 _ProgramId;
        public Int32 ProgramId
        {
            get { return _ProgramId; }
            set { _ProgramId = value; }
        }

        private String _ProgramName;
        public String ProgramName
        {
            get { return _ProgramName; }
            set { _ProgramName = value; }
        }

        private String _AppTerm;
        public String AppTerm
        {
            get { return _AppTerm; }
            set { _AppTerm = value; }
        }

        public Int32 _AppYear;              //possibly change to DateTime type
        public Int32 AppYear
        {
            get { return _AppYear; }
            set { _AppYear = value; }
        }

        private DateTime _DateInputed;
        public DateTime DateInputed
        {
            get { return _DateInputed; }
            set { _DateInputed = value; }
        }

        private DateTime _AppDate;
        public DateTime AppDate
        {
            get { return _AppDate; }
            set { _AppDate = value; }
        }

        private Int32 _TimesViewed;
        public Int32 TimesViewed
        {
            get { return _TimesViewed; }
            set { _TimesViewed = value; }
        }

        private DateTime _LastViewed;
        public DateTime LastViewed
        {
            get { return _LastViewed; }
            set { _LastViewed = value; }
        }

        private Int32? _IsAccepted = null;  //Default = null, 1 = accepted, 0 = denied
        public Int32? IsAccepted            //int32? == Nullable<int32>
        {
            get { return _IsAccepted; }
            set { _IsAccepted = value; }
        }

        private bool _IsFinalized = false;
        public bool IsFinalized
        {
            get { return _IsFinalized; }
            set { _IsFinalized = value; }
        }
        
        #endregion //application section ends


        #region App PreReq / Status section
        
        private Int32 _PreReqId;
        public Int32 PreReqId
        {
            get { return _PreReqId; }
            set { _PreReqId = value; }
        }

        private String _PreReqName;
        private String PreReqName
        {
            get { return _PreReqName; }
            set { _PreReqName = value; }
        }

        private Int32 _PreReqStatusId;
        private Int32 PreReqStatusId
        {
            get { return _PreReqStatusId; }
            set { _PreReqStatusId = value; }
        }

        private PreReqStatusEnum _PreReqStatusName;
        public PreReqStatusEnum PreReqStatusName
        {
            get { return _PreReqStatusName; }
            set { _PreReqStatusName = value; }
        }

        #endregion  //App PreReq / status section ends


        #region Comments section
        
        public const Int32 BodyLength = 512;
        
        private Int32 _CommentId;
        public Int32 CommentId
        {
            get { return _CommentId; }
            set { _CommentId = value; }
        }

        private String _CommentBody;
        public String CommentBody
        {
            get { return _CommentBody; }
            set { _CommentBody = value; }
        }
        
        #endregion  //Comments section ends


        #region PDF section

        private Int32 _PDFId;
        public Int32 PDFId
        {
            get { return _PDFId; }
            set { _PDFId = value; }
        }

        private String _FileName;
        public String FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        //Attribute for Actual file??

        #endregion  //PDF section section ends
    }

    public enum PreReqStatusEnum
    {
        PC = 1,     //PC = Possible Credit
        PT = 2,     //PT = Possible Transfer
        RQ = 3,     //RQ = ReQuired
        NN = 4,     //NN = Not Needed
        RE = 5,     //RE = REcommended
    }               //Default underlying type = int, with the first enumerator starting at 0 implicitly
}