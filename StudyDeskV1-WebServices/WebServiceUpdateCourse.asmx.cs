using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StudyDeskV1_WebServices
{
    /// <summary>
    /// Descripción breve de WebServiceUpdateCourse
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceUpdateCourse : System.Web.Services.WebService
    {

        string uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();

        public WebServiceUpdateCourse()
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
        public string ActualizarCurso(int id, string name, int career_id)
        {
            connection.Open();

            string result;


            MySqlCommand cmd =
                new MySqlCommand("UPDATE bce1wdw4uipazot89sge.courses SET name=@name, career_id=@career_id WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@career_id", career_id);
            try
            {
                cmd.ExecuteNonQuery();
                result = string.Format("A courses with id {0} was updated without problems", id);
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = string.Format("An error occurred while a courses with id {0} was being inserted: {1}", id, ex.ToString());
                connection.Close();
                return result;
            }
        }
    }
}
