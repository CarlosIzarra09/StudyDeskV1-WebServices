using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyDeskV1_WebServices.Helper
{
    public class UserDetails : System.Web.Services.Protocols.SoapHeader
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid() {
            string user = System.Configuration.ConfigurationManager.AppSettings["user"];
            string password = System.Configuration.ConfigurationManager.AppSettings["password"];
            return this.Username == user && this.Password == password;
        }
    }
}