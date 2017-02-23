using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    public class Player
    {
        public Int32 PlayerId { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
