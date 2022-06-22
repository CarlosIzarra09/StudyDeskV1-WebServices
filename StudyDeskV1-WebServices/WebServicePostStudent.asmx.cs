//using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BCryptNet = BCrypt.Net;
using System.Data.SqlClient;

namespace StudyDeskV1_WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServicePostStudent : System.Web.Services.WebService
    {
        string uid, password, server, database;
        private SqlConnection connection;
        //DataSet dataTable = new DataSet();
        public WebServicePostStudent()
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
        public string InsertarEstudiante(string name, string lastName, string logo, string email, string password, int isTutor, int careerId)
        {
            connection.Open();

            string result;

            SqlCommand cmd =
                new SqlCommand("INSERT INTO dbo.students(name, last_name, logo, email, password, is_tutor, career_id) values(@name,@lastname,@logo,@email,@password,@isTutor,@careerid)", connection);
            //cmd.Parameters.AddWithValue("@id",id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@lastname", lastName);
            cmd.Parameters.AddWithValue("@logo", logo);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", BCryptNet.BCrypt.HashPassword(password));
            cmd.Parameters.AddWithValue("@isTutor", isTutor);
            cmd.Parameters.AddWithValue("@careerid", careerId);

            try
            {
                cmd.ExecuteNonQuery();
                result = "A Student was inserted without problems";
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = "An error occurred while a Student was being inserted: " + ex.ToString();
                connection.Close();
                return result;
            }
        }
    }
}
