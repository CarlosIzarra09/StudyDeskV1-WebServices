using StudyDeskV1_WebServices.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyDeskV1_WebServices.Communications
{
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
}