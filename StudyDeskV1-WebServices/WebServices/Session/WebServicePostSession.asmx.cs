using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using StudyDeskV1_WebServices.Communications;
using System.Data.SqlClient;
using System.Web.Services.Protocols;
using StudyDeskV1_WebServices.Helper;

namespace StudyDeskV1_WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServicePostSession : System.Web.Services.WebService
    {
        const string quote = "\"";
        string consulta, uid, password, server, database;
        private SqlConnection connection;
        DataSet dataTable = new DataSet();
        public AuthHeader credentials;

        public WebServicePostSession()
        {
            Initialize();
        }
        private void Initialize()
        {
            server = "sql202201.database.windows.net";
            database = "studydeskDb";
            uid = "STUDYDESK";
            password = "8CL7cR$Ce$gCxNmB";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new SqlConnection(connectionString);
        }

        [WebMethod]
        [SoapHeader("credentials")]
        public WsSecurityResponse InsertarSession(string title, string logo, string description, string startdate, string enddate, int quantitymembers, float price, int tutorid, int platformid, int topicid, int categoryid)
        {
            
            if (credentials != null)
            {
                if (credentials.IsValid())
                {
                    connection.Open();
                    string result;

                    SqlCommand cmd = new SqlCommand("INSERT INTO dbo.sessions(title, logo, description, start_date, end_date, quantity_members, price, tutor_id, platform_id, topic_id, category_id) values(@title, @logo, @description, @startdate, @enddate, @quantitymembers, @price, @tutorid, @platformid, @topicid, @categoryid)", connection);
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
                        return new WsSecurityResponse(null, result);
                    }
                    catch (Exception ex)
                    {
                        result = "An error occurred while a Session was being inserted: " + ex.ToString();
                        connection.Close();
                        return new WsSecurityResponse(result);
                    }


                }
                else
                    return new WsSecurityResponse("Service failed to authenticate against the provided profile credentials. " +
                        "Verify that the SOAP request is using the proper credentials.");
            }
            else
            {
                return new WsSecurityResponse("The selected Security policy either does not have any " +
                    "security profiles (X509 or UserNameToken) or the security profiles are " +
                    "inactive. Verify at least one security profile is active.");
            }
        }
    }
}
