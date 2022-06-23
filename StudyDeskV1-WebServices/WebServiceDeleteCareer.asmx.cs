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
    public class WebServiceDeleteCareer : System.Web.Services.WebService
    {
        const string quote = "\"";
        string consulta, uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();

        public WebServiceDeleteCareer()
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
        public void EliminarCarrera(int id)
        {
            connection.Open();
            string result;
            MySqlCommand cmd = new MySqlCommand("DELETE FROM careers WHERE id=@id ", connection);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.ExecuteNonQuery();
                result = string.Format("An Career with id {0} was deleted without problems", id);
                connection.Close();
            }
            catch (Exception ex)
            {
                result = string.Format("An error occurred while a Career with id {0} was being deleted: {1}", id, ex.ToString());
                connection.Close();
            }
        }
    }
}
