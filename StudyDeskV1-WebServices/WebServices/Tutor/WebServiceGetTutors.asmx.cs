﻿using StudyDeskV1_WebServices.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace StudyDeskV1_WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServiceGetTutors : System.Web.Services.WebService
    {
        const string quote = "\"";
        string consulta, uid, password, server, database;
        private SqlConnection connection;
        readonly DataSet dataTable = new DataSet();
        public AuthHeader credentials;

        public WebServiceGetTutors()
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
        public DataSet ListaTutores()
        {

            if (credentials != null)
            {
                if (credentials.IsValid())
                {
                    connection.Open();
                    consulta = "SELECT * FROM dbo.tutors;";

                    SqlDataAdapter sqlAdapter = new SqlDataAdapter(consulta, connection);
                    sqlAdapter.Fill(dataTable, "Tutors");

                    connection.Close();
                    return dataTable;
                }
                else
                    return dataTable;
            }
            else
            {
                return dataTable;
            }

        }
    }
}
