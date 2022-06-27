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
    public class WebServiceUpdateTopic : System.Web.Services.WebService
    {
        string uid, password, server, database;
        private SqlConnection connection;
        public AuthHeader credentials;
        //DataSet dataTable = new DataSet();
        public WebServiceUpdateTopic()
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
        public WsSecurityResponse ActualizarTopico(int id, string name, int courseId)
        {
            if (credentials != null)
            {
                if (credentials.IsValid())
                {
                    connection.Open();

                    string result;

                    SqlCommand cmd =
                        new SqlCommand("UPDATE dbo.students SET name=@name, course_id=@courseId " +
                        "WHERE id=@id", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@course_id", courseId);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        result = string.Format("An Topic with id {0} was updated without problems", id);
                        connection.Close();
                        return new WsSecurityResponse(null, result);
                    }
                    catch (Exception ex)
                    {
                        result = string.Format("An error occurred while a Topic with id {0} was being inserted: {1}", id, ex.ToString());
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
