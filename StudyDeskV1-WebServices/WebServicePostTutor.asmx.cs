//using MySql.Data.MySqlClient;
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
    /// Descripción breve de WebServicePostTutor
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServicePostTutor : System.Web.Services.WebService
    {
        string uid, password, server, database;
        private SqlConnection connection;
        

        public WebServicePostTutor() {
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
        public string InsertarTutor(string name,string lastName, string description,string logo, string email, string password, double priceperhour, int courseId)
        {
            connection.Open();

            string result;
            

            SqlCommand cmd =
                new SqlCommand("INSERT INTO dbo.tutors(name, last_name, description, logo,email, password,price_per_hour,course_id) values(@name,@lastname,@description,@logo,@email,@password,@priceperhour,@courseid)", connection);
            //cmd.Parameters.AddWithValue("@id",id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@lastname", lastName);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@logo", logo);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", BCryptNet.BCrypt.HashPassword(password));
            cmd.Parameters.AddWithValue("@priceperhour", priceperhour);
            cmd.Parameters.AddWithValue("@courseid", courseId);

            try
            {
                cmd.ExecuteNonQuery();
                result = "An Tutor was inserted without problems"; 
                connection.Close();
                return result;
            }
            catch (Exception ex) {
                result = "An error occurred while a Tutor was being inserted: " + ex.ToString();
                connection.Close();
                return result;
            }
           
        }
    }
}
