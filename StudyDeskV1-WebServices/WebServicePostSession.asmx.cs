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
    public class WebServicePostSession : System.Web.Services.WebService
    {
        const string quote = "\"";
        string consulta, uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();

        public WebServicePostSession()
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
        public void InsertarSession(string title, string logo, string description, string startdate, string enddate, int quantitymembers, float price, int tutorid, int platformid, int topicid, int categoryid)
        {
            connection.Open();
            string result;

            MySqlCommand cmd = new MySqlCommand("INSERT INTO sessions(title, logo, description, start_date, end_date, quantity_members, price, tutor_id, platform_id, topic_id, category_id) values(@title, @logo, @description, @startdate, @enddate, @quantitymembers, @price, @tutorid, @platformid, @topicid, @categoryid)", connection);
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
                result = "An Session was inserted without problems";
                connection.Close();
            }
            catch (Exception ex)
            {
                result = "An error occurred while a Session was being inserted: " + ex.ToString();
                connection.Close();
            }
        }
    }
}
