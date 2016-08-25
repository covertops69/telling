using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telling.Api.Models
{
    public class Game
    {
        public Guid GameId { get; set; }

        public string Name { get; set; }
    }
}