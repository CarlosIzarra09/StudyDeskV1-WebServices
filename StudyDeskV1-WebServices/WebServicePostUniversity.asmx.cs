using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StudyDeskV1_WebServices
{
    /// <summary>
    /// Descripción breve de WebServicePostUniversity
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServicePostUniversity : System.Web.Services.WebService
    {

        
        string uid, password, server, database;
        private MySqlConnection connection;


        public WebServicePostUniversity()
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
        public string InsertarUniversidad(string name)
        {
            connection.Open();

            string result;


            MySqlCommand cmd =
                new MySqlCommand("INSERT INTO universities(name) values(@name)", connection);
            cmd.Parameters.AddWithValue("@name", name);

            try
            {
                cmd.ExecuteNonQuery();
                result = "An University was inserted without problems";
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = "An error occurred while a University was being inserted: " + ex.ToString();
                connection.Close();
                return result;
            }

        }
    }
}
