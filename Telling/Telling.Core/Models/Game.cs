using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    public class Game
    {
        public Int32 GameId { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}