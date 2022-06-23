using StudyDeskV1_WebServices.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyDeskV1_WebServices.Communications
{
    public class AuthenticationResponse : BaseResponse<Authentication>
    {
        public AuthenticationResponse()
        {
        }

        public AuthenticationResponse(string message) : base(message)
        {
        }

        public AuthenticationResponse(Authentication resource, string message) : base(resource, message)
        {
        }
    }
}