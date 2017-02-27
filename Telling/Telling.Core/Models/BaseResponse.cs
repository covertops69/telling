using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    public class BaseResponse<T> where T : class
    {
        public ServiceReponseType ReponseType { get; set; }

        public string Message { get; set; }

        public string ExceptionMessage { get; set; }

        public T Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
