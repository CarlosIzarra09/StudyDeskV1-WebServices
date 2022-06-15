using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StudyDeskV1_WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServiceUpdateStudent : System.Web.Services.WebService
    {
        
        string uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();
        public WebServiceUpdateStudent()
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
        public string ActualizarEstudiante(int id, string name, string lastName, string logo, string email, string password, int tutorId, int careerId)
        {
            connection.Open();

            string result;

            MySqlCommand cmd =
                new MySqlCommand("UPDATE students SET name=@name, last_name=@lastname, logo=@logo,email=@email,password=@password, is_tutor=@tutorId, career_id=@careerId " +
                "WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@lastname", lastName);
            cmd.Parameters.AddWithValue("@logo", logo);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@tutorId", tutorId);
            cmd.Parameters.AddWithValue("@careerId", careerId);

            try
            {
                cmd.ExecuteNonQuery();
                result = string.Format("An Student with id {0} was updated without problems", id);
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = string.Format("An error occurred while a Student with id {0} was being inserted: {1}", id, ex.ToString());
                connection.Close();
                return result;
            }
        }
    }
}
