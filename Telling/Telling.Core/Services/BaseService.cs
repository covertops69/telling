using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Services
{
    public abstract class BaseService
    {
        protected const string API_URL = "http://192.168.0.175/Telling.Api/api";

        protected IRestService RestService { get; private set; }

        protected BaseService(IRestService restService)
        {
            RestService = restService;
        }
    }
}
