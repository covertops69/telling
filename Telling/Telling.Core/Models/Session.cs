using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.ViewModels;

namespace Telling.Core.Models
{
    public class Session : MvxViewModel
    {
        public int SessionId { get; set; }

        public Game Game { get; set; }

        public Player Player { get; set; }
        public DateTime SessionDate { get; set; }
        public string Venue { get; set; }

        //public List<Player> Players { get; set; }

        //public Session()
        //{
        //    Players = new List<Player>();
        //}
    }
}
