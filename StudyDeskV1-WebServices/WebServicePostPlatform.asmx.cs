using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StudyDeskV1_WebServices
{
    /// <summary>
    /// Summary description for WebServicePostPlatform
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServicePostPlatform : System.Web.Services.WebService
    {
        string uid, password, server, database;
        private MySqlConnection connection;


        public WebServicePostPlatform()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "bce1wdw4uipazot89sge-mysql.services.clever-cloud.com";
            database = "bce1wdw4uipazot89sge";
            uid = "uq0dnd7aah5zgpja";
            password = "POOf7hGH9xOVGw4DNLT7";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        [WebMethod]
        public string InsertarPlataforma(string name, string platform_url)
        {
            connection.Open();
            string result;

            MySqlCommand cmd = new MySqlCommand("INSERT INTO bce1wdw4uipazot89sge.platforms(platform_url,name) values(@platform_url,@name)", connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@platform_url", platform_url);
            try
            {
                cmd.ExecuteNonQuery();
                result = "A Platform was inserted without problems";
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = "An error occurred while a Platform was being inserted: " + ex.ToString();
                connection.Close();
                return result;
            }
        }
    }
}
