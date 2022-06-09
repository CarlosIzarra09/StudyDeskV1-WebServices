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
    public class WebServicePostStudent : System.Web.Services.WebService
    {
        const string quote = "\"";
        string consulta, uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();
        public WebServicePostStudent()
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
        public string InsertarEstudiante(string name, string lastName, string logo, string email, string password, int tutorId, int careerId)
        {
            connection.Open();

            string result;

            MySqlCommand cmd =
                new MySqlCommand("INSERT INTO students(name, last_name, logo, email, password, is_tutor, career_id) values(@name,@lastname,@logo,@email,@password,@tutorid,@careerid)", connection);
            //cmd.Parameters.AddWithValue("@id",id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@lastname", lastName);
            cmd.Parameters.AddWithValue("@logo", logo);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@tutorid", tutorId);
            cmd.Parameters.AddWithValue("@careerid", careerId);

            try
            {
                cmd.ExecuteNonQuery();
                result = "An Student was inserted without problems";
                return result;
            }
            catch (Exception ex)
            {
                result = "An error occurred while a Tutor was being inserted: " + ex.ToString();
                return result;
            }
        }
    }
}
