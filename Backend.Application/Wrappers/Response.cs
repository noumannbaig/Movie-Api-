
using System.Collections.Generic;
using Backend.Application.Exceptions;

namespace Backend.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string message = "")
        {
            Message = message;
            PayLoad = data;
            
        }
        public Response(bool success, T data, string message = "")
        {
            Message = message;
            PayLoad = data;
            this.success = success;

        }
        //public List<ErrorModel> Errors { get; set; }
        public string Message { get; set; }
        public T PayLoad { get; set; }
        public List<ErrorModel> errors { get; set; }
        public bool success { get; set; }
    }
}
