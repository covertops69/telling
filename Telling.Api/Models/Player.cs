﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telling.Api.Models
{
    public class Player
    {
        public Guid PlayerId { get; set; }

        public string Name { get; set; }
    }
}