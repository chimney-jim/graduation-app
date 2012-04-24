using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GraduateAppProcessing
{
    //
    //Read-only class for connection string object(s)
    //Reconfigure global web.config for additional connection string(s)
    //
    public class ConnectionStringClass
    {
        //DB_name = test (mysql)
        private String _connStr1 = ConfigurationManager.ConnectionStrings["MySQLConnection1"].ToString();

        public String ConnStr1
        {
            get { return _connStr1; }
        }

        //DB_name = application (mysql)
        //USE THIS
        private String _connStr2 = ConfigurationManager.ConnectionStrings["MySQLConnection2"].ToString();

        public String ConnStr2
        {
            get { return _connStr2; }
        }
    }
}