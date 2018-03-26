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
                                SessionId = Convert.ToInt32(reader["SessionId"].ToString()),
                                SessionDate = DateTime.Parse(reader["SessionDate"].ToString()),
                                Venue = reader["Venue"].ToString(),

                                Game = new Game {
                                    GameId = Convert.ToInt32(reader["GameId"].ToString()),
                                    Name = reader["GameName"].ToString(),
                                    ImageName = reader["ImageName"].ToString(),
                                },
                            });
                        }
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // GET: api/Sessions/83642E19-C56A-E611-B37C-00155D291606
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sessions
        public void Post([FromBody]Session session)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                try
                {
                    connection.Open();

                    session.SessionId = InsertSession(session, connection);
                    InsertPlayers(session, connection);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static Int32 InsertSession(Session session, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("spInsertSession", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 5;

            command.Parameters.Add("@GameId", SqlDbType.Int).Value = session.Game.GameId;
            command.Parameters.Add("@SessionDate", SqlDbType.Date).Value = session.SessionDate;
            command.Parameters.Add("@Venue", SqlDbType.NVarChar).Value = session.Venue;

            return Convert.ToInt32(command.ExecuteScalar().ToString());
        }

        private static void InsertPlayers(Session session, SqlConnection connection)
        {
            foreach (Player p in session.Players)
            {
                SqlCommand command = new SqlCommand("spInsertSessionPlayer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 5;

                command.Parameters.Add("@PlayerId", SqlDbType.Int).Value = p.PlayerId;
                command.Parameters.Add("@SessionId", SqlDbType.Int).Value = session.SessionId;

                command.ExecuteNonQuery();
            }
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
