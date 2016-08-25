﻿using System;
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
    public class GamesController : ApiController
    {
        // GET: api/Games
        public IEnumerable<Game> Get()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=41.134.220.74;Initial Catalog=Telling;Persist Security Info=True;User ID=sa;Password=B3achball;"))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetGames", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 5;

                    SqlDataReader reader = command.ExecuteReader();

                    var response = new List<Game>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            response.Add(new Game
                            {
                                GameId = Guid.Parse(reader["GameId"].ToString()),
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

                //return new List<Game>()
                //{
                //    new Game { Name = "Game 1" }
                //};
            }
        }

        // GET: api/Games/83642E19-C56A-E611-B37C-00155D291606
        public Game Get(Guid id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=41.134.220.74;Initial Catalog=Telling;Persist Security Info=True;User ID=sa;Password=B3achball;"))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spGetGame", connection);
                    command.Parameters.Add("@GameId", SqlDbType.UniqueIdentifier).Value = id;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 5;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        return new Game
                        {
                            GameId = Guid.Parse(reader["GameId"].ToString()),
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

                //return new List<Game>()
                //{
                //    new Game { Name = "Game 1" }
                //};
            }
        }

        // POST: api/Games
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Games/83642E19-C56A-E611-B37C-00155D291606
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Games/83642E19-C56A-E611-B37C-00155D291606
        public void Delete(int id)
        {
        }
    }
}