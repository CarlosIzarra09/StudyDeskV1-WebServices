//using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BCryptNet = BCrypt.Net;
using System.Data.SqlClient;
using StudyDeskV1_WebServices.Helper;
using System.Web.Services.Protocols;
using StudyDeskV1_WebServices.Communications;

namespace StudyDeskV1_WebServices.WebServices.Topic
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServicePostTopic : System.Web.Services.WebService
    {
        string uid, password, server, database;
        private SqlConnection connection;
        public AuthHeader credentials;
        //DataSet dataTable = new DataSet();
        public WebServicePostTopic()
        {
            Initialize();
        }
        private void Initialize()
        {
            server = "sql202201.database.windows.net";
            database = "studydeskDb";
            uid = "STUDYDESK";
            password = "8CL7cR$Ce$gCxNmB";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new SqlConnection(connectionString);
        }
        [WebMethod]
        [SoapHeader("credentials")]
        public WsSecurityResponse InsertarTopico(string name, int courseId)
        {
            if (credentials != null)
            {
                if (credentials.IsValid())
                {
                    connection.Open();

                    string result;

                    SqlCommand cmd =
                        new SqlCommand("INSERT INTO dbo.topics(name, course_id) values(@name,@courseId)", connection);
                    //cmd.Parameters.AddWithValue("@id",id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@courseId", courseId);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        result = "A Topic was inserted without problems";
                        connection.Close();
                        return new WsSecurityResponse(null, result);
                    }
                    catch (Exception ex)
                    {
                        result = "An error occurred while a Topic was being inserted: " + ex.ToString();
                        connection.Close();
                        return new WsSecurityResponse(result);
                    }

                }
                else
                    return new WsSecurityResponse("Service failed to authenticate against the provided profile credentials. " +
                        "Verify that the SOAP request is using the proper credentials.");
            }
            else
            {
                return new WsSecurityResponse("The selected Security policy either does not have any " +
                    "security profiles (X509 or UserNameToken) or the security profiles are " +
                    "inactive. Verify at least one security profile is active.");
            }
        }
    }
}
