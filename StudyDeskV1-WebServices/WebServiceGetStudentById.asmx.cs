using MySql.Data.MySqlClient;
using StudyDeskV1_WebServices.Communications;
using StudyDeskV1_WebServices.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StudyDeskV1_WebServices
{
    public class StudentResponse : BaseResponse<Student>
    {
        public StudentResponse()
        {
        }

        public StudentResponse(string message) : base(message)
        {
        }

        public StudentResponse(Student resource, string message) : base(resource, message)
        {
        }
    }





    /// <summary>
    /// Descripción breve de GetStudentById
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceGetStudentById : System.Web.Services.WebService
    {
        string consulta, uid, password, server, database;
        private MySqlConnection connection;
        DataSet dataTable = new DataSet();

        public WebServiceGetStudentById()
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
        public StudentResponse RetornarUsuarioEstudiantePorId(int id)
        {
            connection.Open();
            Student student = new Student();
            StudentResponse studentResponse;

            consulta = String.Format("SELECT * FROM students WHERE id='{0}'", id);
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(consulta, connection);
            sqlAdapter.Fill(dataTable, "RetornaEstudiante");

            if (dataTable.Tables["RetornaEstudiante"].Rows.Count > 0)
            {
                student.Id = Convert.ToInt32(dataTable.Tables["RetornaEstudiante"].Rows[0]["id"]);
                student.Name = Convert.ToString(dataTable.Tables["RetornaEstudiante"].Rows[0]["name"]);
                student.LastName = Convert.ToString(dataTable.Tables["RetornaEstudiante"].Rows[0]["last_name"]);
                student.Logo = Convert.ToString(dataTable.Tables["RetornaEstudiante"].Rows[0]["logo"]);
                student.Email = Convert.ToString(dataTable.Tables["RetornaEstudiante"].Rows[0]["email"]);
                student.isTutor = Convert.ToBoolean(dataTable.Tables["RetornaEstudiante"].Rows[0]["is_tutor"]);
                student.CareerId = Convert.ToInt32(dataTable.Tables["RetornaEstudiante"].Rows[0]["career_id"]);

                studentResponse = new StudentResponse(student, "");
            }
            else
            {
                studentResponse = new StudentResponse("Student not found with id " + id.ToString());
            }


            connection.Close();
            return studentResponse;


        }

    }
}
