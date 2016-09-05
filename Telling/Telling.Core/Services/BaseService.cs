using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Services
{
    public abstract class BaseService
    {
        protected const string API_URL = "http://telling.fullstack.co.za/api";

        protected IRestService RestService { get; private set; }

        protected BaseService(IRestService restService)
        {
            RestService = restService;
        }

        //protected static void AddParamsVersion(Dictionary<string, object> parameters, int versionNumber = 1)
        //{
        //    parameters.Add("version", versionNumber);
        //}
    }
}