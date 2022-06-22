﻿//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StudyDeskV1_WebServices
{
    /// <summary>
    /// Summary description for WebServiceGetPlatforms
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceGetPlatforms : System.Web.Services.WebService
    {
        //const string quote = "\"";
        string consulta, uid, password, server, database;
        private SqlConnection connection;
        readonly DataSet dataTable = new DataSet();

        public WebServiceGetPlatforms()
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
        public DataSet ListaPlataformas()
        {
            connection.Open();


            consulta = "SELECT * FROM dbo.platforms;";

            SqlDataAdapter sqlAdapter = new SqlDataAdapter(consulta, connection);
            sqlAdapter.Fill(dataTable, "DevuelveLista");

            connection.Close();
            return dataTable;
        }
    }
}
