using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using BCryptNet = BCrypt.Net;

namespace StudyDeskV1_WebServices
{
    public class ResponseService
    {
        public ResponseService() { }
        public ResponseService(string message, string status)
        {
            this.Status = status;
            this.Message = message;
        }

        public string Status { get; set; }
        public string Message { get; set; }
    }

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
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();

        public WebServiceAuthentication()
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
        public ResponseService AutenticarUsuarioTutor(string email, string password)
        {
            connection.Open();


            consulta = String.Format("SELECT * FROM tutors WHERE email='{0}'", email);

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(consulta, connection);
            sqlAdapter.Fill(dataTable, "RetornaTutor");

            if (dataTable.Tables["RetornaTutor"].Rows.Count > 0)
            {

                object data = dataTable.Tables["RetornaTutor"].Rows[0]["password"];
                string requestPassword = Convert.ToString(data);

                if (!BCryptNet.BCrypt.Verify(password, requestPassword))
                {
                    connection.Close();
                    return new ResponseService("Invalid password for this user", "error");
                }

                else
                {
                    connection.Close();
                    return new ResponseService("Successful tutor authentication, welcome", "success");
                }

            }
            else
            {
                connection.Close();
                return new ResponseService("This email does not correspond to any user", "error");
            }

        }

        [WebMethod]
        public ResponseService AutenticarUsuarioEstudiante(string email, string password)
        {
            connection.Open();


            consulta = String.Format("SELECT * FROM students WHERE email='{0}'", email);

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(consulta, connection);
            sqlAdapter.Fill(dataTable, "RetornaEstudiante");

            if (dataTable.Tables["RetornaEstudiante"].Rows.Count > 0)
            {

                object data = dataTable.Tables["RetornaEstudiante"].Rows[0]["password"];
                string requestPassword = Convert.ToString(data);

                if (!BCryptNet.BCrypt.Verify(password, requestPassword))
                {
                    connection.Close();
                    return new ResponseService("Invalid password for this user", "error");
                }

                else
                {
                    connection.Close();
                    return new ResponseService("Successful student authentication, welcome", "success");
                }

            }
            else
            {
                connection.Close();
                return new ResponseService("This email does not correspond to any user", "error");
            }

        }
    }
}
