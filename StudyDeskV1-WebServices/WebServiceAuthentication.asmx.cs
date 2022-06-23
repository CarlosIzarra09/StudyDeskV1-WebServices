//using MySql.Data.MySqlClient;
using StudyDeskV1_WebServices.Communications;
using StudyDeskV1_WebServices.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using BCryptNet = BCrypt.Net;

namespace StudyDeskV1_WebServices
{
    
    /// <summary>
    /// Descripción breve de WebServiceAuthentication
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceAuthentication : System.Web.Services.WebService
    {

        string consulta, uid, password, server, database;
        private SqlConnection connection;
        readonly DataSet dataTable = new DataSet();
        //public AuthenticationResponse response;

        public WebServiceAuthentication()
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
        public AuthenticationResponse AutenticarUsuarioTutor(string email, string password)
        {
            connection.Open();


            consulta = String.Format("SELECT * FROM dbo.tutors WHERE email='{0}'", email);

            SqlDataAdapter sqlAdapter = new SqlDataAdapter(consulta, connection);
            sqlAdapter.Fill(dataTable, "RetornaTutor");

            if (dataTable.Tables["RetornaTutor"].Rows.Count > 0)
            {

                string requestPassword = Convert.ToString(dataTable.Tables["RetornaTutor"].Rows[0]["password"]);

                if (!BCryptNet.BCrypt.Verify(password, requestPassword))
                {
                    connection.Close();
                    //return new ResponseService("Invalid password for this user", "Error");
                    return new AuthenticationResponse("Invalid password for this user");
                }

                else
                {
                    connection.Close();
                    //return new ResponseService("Successful tutor authentication, welcome", "Success");
                    Authentication auth = new Authentication();
                    auth.Id = Convert.ToInt32(dataTable.Tables["RetornaTutor"].Rows[0]["id"]);
                    return new AuthenticationResponse(auth, "Successful tutor authentication, welcome");
                }

            }
            else
            {
                connection.Close();
                //return new ResponseService("This email does not correspond to any user", "Error");
                return new AuthenticationResponse("This email does not correspond to any user");
            }

        }

        [WebMethod]
        public AuthenticationResponse AutenticarUsuarioEstudiante(string email, string password)
        {
            connection.Open();


            consulta = String.Format("SELECT * FROM dbo.students WHERE email='{0}'", email);

            SqlDataAdapter sqlAdapter = new SqlDataAdapter(consulta, connection);
            sqlAdapter.Fill(dataTable, "RetornaEstudiante");

            if (dataTable.Tables["RetornaEstudiante"].Rows.Count > 0)
            {

                object data = dataTable.Tables["RetornaEstudiante"].Rows[0]["password"];
                string requestPassword = Convert.ToString(data);

                if (!BCryptNet.BCrypt.Verify(password, requestPassword))
                {
                    connection.Close();
                    //return new ResponseService("Invalid password for this user", "Error");
                    return new AuthenticationResponse("Invalid password for this user");
                }

                else
                {
                    connection.Close();
                    //return new ResponseService("Successful student authentication, welcome", "Success");
                    Authentication auth = new Authentication();
                    auth.Id = Convert.ToInt32(dataTable.Tables["RetornaEstudiante"].Rows[0]["id"]);
                    return new AuthenticationResponse(auth,"Successful student authentication, welcome");
                }

            }
            else
            {
                connection.Close();
                return new AuthenticationResponse("This email does not correspond to any user");
            }

        }
    }
}
