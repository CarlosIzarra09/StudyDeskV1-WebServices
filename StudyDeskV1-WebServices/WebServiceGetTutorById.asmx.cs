//using MySql.Data.MySqlClient;
using StudyDeskV1_WebServices.Communications;
using StudyDeskV1_WebServices.Resources;
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
    /// Descripción breve de WebServiceGetTutorById
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]

    public class TutorResponse : BaseResponse<Tutor>
    {
        public TutorResponse()
        {
        }

        public TutorResponse(string message) : base(message)
        {
        }

        public TutorResponse(Tutor resource, string message) : base(resource, message)
        {
        }
    }



    public class WebServiceGetTutorById : System.Web.Services.WebService
    {
        string consulta, uid, password, server, database;
        private SqlConnection connection;
        readonly DataSet dataTable = new DataSet();

        public WebServiceGetTutorById()
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
        public TutorResponse RetornarUsuarioTutorPorId(int id)
        {
            connection.Open();
            Tutor tutor = new Tutor();
            TutorResponse tutorResponse;

            consulta = String.Format("SELECT * FROM dbo.tutors WHERE id='{0}'", id);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(consulta, connection);
            sqlAdapter.Fill(dataTable, "RetornaTutor");

            if (dataTable.Tables["RetornaTutor"].Rows.Count > 0)
            {
                tutor.Id = Convert.ToInt32(dataTable.Tables["RetornaTutor"].Rows[0]["id"]);
                tutor.Name = Convert.ToString(dataTable.Tables["RetornaTutor"].Rows[0]["name"]);
                tutor.LastName = Convert.ToString(dataTable.Tables["RetornaTutor"].Rows[0]["last_name"]);
                tutor.Description = Convert.ToString(dataTable.Tables["RetornaTutor"].Rows[0]["description"]);
                tutor.Logo = Convert.ToString(dataTable.Tables["RetornaTutor"].Rows[0]["logo"]);
                tutor.Email = Convert.ToString(dataTable.Tables["RetornaTutor"].Rows[0]["email"]);
                tutor.PricePerHour = Convert.ToDouble(dataTable.Tables["RetornaTutor"].Rows[0]["price_per_hour"]);
                tutor.CourseId = Convert.ToInt32(dataTable.Tables["RetornaTutor"].Rows[0]["course_id"]);

                tutorResponse = new TutorResponse(tutor, "");
            }
            else {
                tutorResponse = new TutorResponse("Tutor not found with id " + id.ToString());
            }
            

            connection.Close();
            return tutorResponse;

            
        }
    }
}
