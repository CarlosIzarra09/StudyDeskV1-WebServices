using StudyDeskV1_WebServices.Communications;
using StudyDeskV1_WebServices.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace StudyDeskV1_WebServices.WebServices.SessionReservation
{
    /// <summary>
    /// Descripción breve de WebServicePostSessionRsv
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServicePostSessionRsv : System.Web.Services.WebService
    {

        string consulta, uid, password, server, database;
        private SqlConnection connection;
        readonly DataSet dataTable = new DataSet();
        public AuthHeader credentials;

        public WebServicePostSessionRsv()
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
        public WsSecurityResponse InsertarSesionReservada(int studentId,int sessionId,int qualification,bool confirmed)
        {
            if (credentials != null)
            {
                if (credentials.IsValid())
                {
                    connection.Open();
                    string result;

                    SqlCommand cmd = new SqlCommand("INSERT INTO dbo.session_reservations(student_id, session_id,qualification, confirmed) values(@studentId, @sessionId,@qualification,@confirmed)", connection);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@sessionId", sessionId);
                    cmd.Parameters.AddWithValue("@qualification", sessionId);
                    cmd.Parameters.AddWithValue("@confirmed", confirmed);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        result = "A Session Reservation was inserted without problems";
                        connection.Close();
                        return new WsSecurityResponse(null, result);
                    }
                    catch (Exception ex)
                    {
                        result = "An error occurred while a Session Reservation was being inserted: " + ex.ToString();
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
