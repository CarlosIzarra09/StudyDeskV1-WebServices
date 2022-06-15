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
    /// Descripción breve de WebServiceGetTutors
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceUpdateCategory : System.Web.Services.WebService
    {

        
        string uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();

        public WebServiceUpdateCategory()
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
        public string ActualizarCategoria(int id,string name)
        {
            connection.Open();

            string result;


            MySqlCommand cmd =
                new MySqlCommand("UPDATE bce1wdw4uipazot89sge.categories SET name=@name WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);

            try
            {
                cmd.ExecuteNonQuery();
                result = string.Format("A category with id {0} was updated without problems", id);
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = string.Format("An error occurred while a category with id {0} was being inserted: {1}", id, ex.ToString());
                connection.Close();
                return result;
            }
        }
    }
}
