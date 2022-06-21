using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyDeskV1_WebServices.Resources
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public bool isTutor { get; set; }

        public int CareerId { get; set; }
    }
}