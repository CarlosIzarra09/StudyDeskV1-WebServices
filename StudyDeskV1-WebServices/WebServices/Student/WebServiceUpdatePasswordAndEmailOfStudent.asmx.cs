using StudyDeskV1_WebServices.Communications;
using StudyDeskV1_WebServices.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using BCryptNet = BCrypt.Net;

namespace StudyDeskV1_WebServices.WebServices.Student
{
    /// <summary>
    /// Descripción breve de WebServiceUpdatePasswordAndEmailOfStudent
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceUpdatePasswordAndEmailOfStudent : System.Web.Services.WebService
    {

        string uid, password, server, database;
        private SqlConnection connection;
        public AuthHeader credentials;

        public WebServiceUpdatePasswordAndEmailOfStudent()
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
        public WsSecurityResponse ActualizarTutor(int id, string password, string mail)
        {
            if (credentials != null)
            {
                if (credentials.IsValid())
                {
                    connection.Open();

                    string result;


                    SqlCommand cmd =
                        new SqlCommand("UPDATE dbo.tutors SET email=@email, password=@password " +
                        "WHERE id=@id", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@email", mail);
                    cmd.Parameters.AddWithValue("@password", BCryptNet.BCrypt.HashPassword(password));

                    try
                    {
                        cmd.ExecuteNonQuery();
                        result = string.Format("An Student with id {0} was updated without problems", id);
                        connection.Close();
                        return new WsSecurityResponse(null, result);
                    }
                    catch (Exception ex)
                    {
                        result = string.Format("An error occurred while a Student with id {0} was being inserted: {1}", id, ex.ToString());
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
