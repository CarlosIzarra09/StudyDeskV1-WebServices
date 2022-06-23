using StudyDeskV1_WebServices.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyDeskV1_WebServices.Communications
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
}