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
    public class WebServiceGetStudents : System.Web.Services.WebService
    {
        
        string consulta, uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();
        public WebServiceGetStudents()
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
        public DataSet ListaEstudiantes()
        {
            connection.Open();

            consulta = "SELECT * FROM bce1wdw4uipazot89sge.students";

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(consulta, connection);
            sqlAdapter.Fill(dataTable, "DevuelveLista");
            
            connection.Close();
            return dataTable;
        }
    }
}
