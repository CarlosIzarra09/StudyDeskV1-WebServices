using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StudyDeskV1_WebServices
{
    /// <summary>
    /// Descripción breve de WebServicePostCourse
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServicePostCourse : System.Web.Services.WebService
    {

        string uid, password, server, database;
        private MySqlConnection connection;


        public WebServicePostCourse()
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
        public string InsertarCurso(string name, int id)
        {
            connection.Open();
            string result;

            MySqlCommand cmd = new MySqlCommand("INSERT INTO bce1wdw4uipazot89sge.courses(name, career_id) values(@name, @career_id)", connection);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@career_id", id);
            try
            {
                cmd.ExecuteNonQuery();
                result = "A Course was inserted without problems";
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = "An error occurred while a Course was being inserted: " + ex.ToString();
                connection.Close();
                return result;
            }
        }
    }
}
