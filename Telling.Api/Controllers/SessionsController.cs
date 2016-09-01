using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Telling.Api.Models;

namespace Telling.Api.Controllers
{
    public class SessionsController : ApiController
    {
        // GET: api/Sessions
        public IEnumerable<Session> Get()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetSessions", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 5;

                    SqlDataReader reader = command.ExecuteReader();

                    var response = new List<Session>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            response.Add(new Session
                            {
                                SessionId = Guid.Parse(reader["SessionId"].ToString()),
                                GameId = Guid.Parse(reader["GameId"].ToString()),
                                GameName = reader["GameName"].ToString(),
                                ImageName = reader["ImageName"].ToString(),
                                SessionDate = DateTime.Parse(reader["SessionDate"].ToString())
                            });
                        }
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //return new List<Game>()
                //{
                //    new Game { Name = "Game 1" }
                //};
            }
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
