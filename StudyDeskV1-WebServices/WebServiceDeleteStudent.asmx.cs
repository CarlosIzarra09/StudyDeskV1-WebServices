//using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace StudyDeskV1_WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServiceDeleteStudent : System.Web.Services.WebService
    {
        
        string uid, password, server, database;
        private SqlConnection connection;
        //readonly DataSet dataTable = new DataSet();
        public WebServiceDeleteStudent()
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
        public string EliminarEstudiante(int id)
        {
            connection.Open();
            string result;
            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.students WHERE id=@id ", connection);
            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                cmd.ExecuteNonQuery();
                result = string.Format("An Student with id {0} was deleted without problems", id);
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                result = string.Format("An error occurred while a Student with id {0} was being deleted: {1}", id, ex.ToString());
                connection.Close();
                return result;
            }
        }
    }
}
