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
    public class WebServiceUpdatesSession : System.Web.Services.WebService
    {
        const string quote = "\"";
        string consulta, uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();

        public WebServiceUpdatesSession()
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
        public void ActualizarSession(int id, string title, string logo, string description, string startdate, string enddate, int quantitymembers, float price, int tutorid, int platformid, int topicid, int categoryid)
        {
            connection.Open();

            string result;

            MySqlCommand cmd = new MySqlCommand("UPDATE sessions SET title=@title, logo=@logo, description=@description, start_date=@startdate, end_date=@enddate, quantity_members=@quantitymembers, price=@price, tutor_id=@tutorid, platform_id=@platformid, topic_id=@topicid, category_id=@categoryid WHERE id=@id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@logo", logo);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@startdate", startdate);
            cmd.Parameters.AddWithValue("@enddate", enddate);
            cmd.Parameters.AddWithValue("@quantitymembers", quantitymembers);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@tutorid", tutorid);
            cmd.Parameters.AddWithValue("@platformid", platformid);
            cmd.Parameters.AddWithValue("@topicid", topicid);
            cmd.Parameters.AddWithValue("@categoryid", categoryid);

            try
            {
                cmd.ExecuteNonQuery();
                result = string.Format("An Session with id {0} was updated without problems", id);
                connection.Close();
            }
            catch (Exception ex)
            {
                result = string.Format("An error occurred while a Session with id {0} was being inserted: {1}", id, ex.ToString());
                connection.Close();
            }
        }
    }
}
