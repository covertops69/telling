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
    public class PlayersController : ApiController
    {
        // GET: api/Players
        public IEnumerable<Player> Get()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetPlayers", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 5;

                    SqlDataReader reader = command.ExecuteReader();

                    var response = new List<Player>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            response.Add(new Player
                            {
                                PlayerId = Convert.ToInt32(reader["PlayerId"].ToString()),
                                Name = reader["Name"].ToString()
                            });
                        }
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //return new List<Player>()
                //{
                //    new Player { Name = "Player 1" }
                //};
            }
        }

        // GET: api/Players/83642E19-C56A-E611-B37C-00155D291606
        public Player Get(Int32 id)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetPlayer", connection);
                    command.Parameters.Add("@PlayerId", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 5;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        return new Player
                        {
                            PlayerId = Convert.ToInt32(reader["PlayerId"].ToString()),
                            Name = reader["Name"].ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //return new List<Player>()
                //{
                //    new Player { Name = "Player 1" }
                //};
            }
        }

        // POST: api/Players
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Players/83642E19-C56A-E611-B37C-00155D291606
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Players/83642E19-C56A-E611-B37C-00155D291606
        public void Delete(int id)
        {
        }
    }
}
