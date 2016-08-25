using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Telling.Api.Controllers
{
    public class SessionsController : ApiController
    {
        // GET: api/Sessions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sessions/83642E19-C56A-E611-B37C-00155D291606
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sessions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Sessions/83642E19-C56A-E611-B37C-00155D291606
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sessions/83642E19-C56A-E611-B37C-00155D291606
        public void Delete(int id)
        {
        }
    }
}
