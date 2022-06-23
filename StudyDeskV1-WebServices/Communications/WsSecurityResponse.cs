using StudyDeskV1_WebServices.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyDeskV1_WebServices.Communications
{
    public class WsSecurityResponse : BaseResponse<WsSecurity>
    {
        public WsSecurityResponse()
        {
        }

        public WsSecurityResponse(string message) : base(message)
        {
        }

        public WsSecurityResponse(WsSecurity resource, string message) : base(resource, message)
        {
        }
    }
}