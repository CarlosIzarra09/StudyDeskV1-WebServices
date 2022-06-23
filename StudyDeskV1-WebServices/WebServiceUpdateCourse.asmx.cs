﻿//using MySql.Data.MySqlClient;
using StudyDeskV1_WebServices.Communications;
using StudyDeskV1_WebServices.Helper;
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
    /// <summary>
    /// Descripción breve de WebServiceUpdateCourse
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceUpdateCourse : System.Web.Services.WebService
    {

        string uid, password, server, database;
        private SqlConnection connection;
        public AuthHeader credentials;
        //DataSet dataTable = new DataSet();

        public WebServiceUpdateCourse()
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
        public WsSecurityResponse ActualizarCurso(int id, string name, int career_id)
        {
            if (credentials != null)
            {
                if (credentials.IsValid())
                {
                    connection.Open();

                    string result;


                    SqlCommand cmd =
                        new SqlCommand("UPDATE dbo.courses SET name=@name, career_id=@career_id WHERE id=@id", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@career_id", career_id);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        result = string.Format("A courses with id {0} was updated without problems", id);
                        connection.Close();
                        return new WsSecurityResponse(null,result);
                    }
                    catch (Exception ex)
                    {
                        result = string.Format("An error occurred while a course with id {0} was being updated: {1}", id, ex.ToString());
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
