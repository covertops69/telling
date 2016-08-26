﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public DateTime SessionDate { get; set; }
    }
}
