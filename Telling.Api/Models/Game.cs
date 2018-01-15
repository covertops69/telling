using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telling.Api.Models
{
    public class Game
    {
        public Int32 GameId { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }
    }
}