using StudyDeskV1_WebServices.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyDeskV1_WebServices.Communications
{
    public abstract class BaseResponse<T>
    {
      
        public bool Success { get; set; }
        public string Message { get;  set; }
        public T Resource { get; set; }

        public BaseResponse() { }
        public BaseResponse(T resource, string message)
        {
            Resource = resource;
            Success = true;
            Message = message;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }

    }
}