using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduateAppProcessing
{
    public class Secretary
    {
        public Secretary(int idNum, string userType, string firstName, string lastName)
        {
            id = idNum;
            type = userType;
            fName = firstName;
            lName = lastName;
        } 

        private int id;
        private string type;
        private string fName;
        private string lName;
    }
}