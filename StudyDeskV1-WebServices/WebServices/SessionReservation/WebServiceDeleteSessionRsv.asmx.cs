﻿using StudyDeskV1_WebServices.Communications;
using StudyDeskV1_WebServices.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace StudyDeskV1_WebServices.WebServices.SessionReservation
{
    /// <summary>
    /// Descripción breve de WebServiceDeleteSessionRsv
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceDeleteSessionRsv : System.Web.Services.WebService
    {

        string consulta, uid, password, server, database;
        private SqlConnection connection;
        readonly DataSet dataTable = new DataSet();
        public AuthHeader credentials;

        public WebServiceDeleteSessionRsv()
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
        public WsSecurityResponse EliminarSesionReservada(int id)
        {
            if (credentials != null)
            {
                if (credentials.IsValid())
                {
                    connection.Open();
                    string result;
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.session_reservations WHERE id=@id ", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        result = string.Format("A Session Reservation with id {0} was deleted without problems", id);
                        connection.Close();
                        return new WsSecurityResponse(null, result);
                    }
                    catch (Exception ex)
                    {
                        result = string.Format("An error occurred while a Session Reservation with id {0} was being deleted: {1}", id, ex.ToString());
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
