using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telling.Api.Models
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public DateTime SessionDate { get; set; }
    }
}