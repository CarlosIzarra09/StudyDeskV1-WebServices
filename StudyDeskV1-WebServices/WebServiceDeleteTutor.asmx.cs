using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StudyDeskV1_WebServices
{
    /// <summary>
    /// Descripción breve de WebServiceDeleteTutor
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceDeleteTutor : System.Web.Services.WebService
    {

        const string quote = "\"";
        string consulta, uid, password, server, database;
        private MySqlConnection connection;


        public WebServiceDeleteTutor()
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
        public string ActualizarTutor(int id) {

            connection.Open();
            string result;
            MySqlCommand cmd = new MySqlCommand("DELETE FROM tutors WHERE id=@id ", connection);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.ExecuteNonQuery();
                result = string.Format("An Tutor with id {0} was deleted without problems", id);
                return result;
            }
            catch (Exception ex)
            {
                result = string.Format("An error occurred while a Tutor with id {0} was being deleted: {1}",id,ex.ToString());
                return result;
            }
        }
    }
}
