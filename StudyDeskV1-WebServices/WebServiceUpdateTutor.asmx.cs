//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using BCryptNet = BCrypt.Net;

namespace StudyDeskV1_WebServices
{
    /// <summary>
    /// Descripción breve de WebServiceUpdateTutor
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceUpdateTutor : System.Web.Services.WebService
    {
        
        string uid, password, server, database;
        private SqlConnection connection;

        public WebServiceUpdateTutor()
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
        public string ActualizarTutor(int id, string name, string lastName, string description, string logo, string email, string password, double priceperhour, int courseId)
        {
            connection.Open();

            string result;


            SqlCommand cmd =
                new SqlCommand("UPDATE dbo.tutors SET name=@name, last_name=@lastname, description=@description,logo=@logo,email=@email,password=@password,price_per_hour=@priceperhour,course_id=@courseid " +
                "WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("@id",id);
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
                result = string.Format("An Tutor with id {0} was updated without problems",id);
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = string.Format("An error occurred while a Tutor with id {0} was being inserted: {1}",id,ex.ToString());
                connection.Close();
                return result;
            }

        }

    }
}
