using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telling.Api.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public Game Game { get; set; }
        public DateTime SessionDate { get; set; }
        public string Venue { get; set; }

        public List<Player> Players { get; set; }

        public Session()
        {
            Players = new List<Player>();
        }
    }
}