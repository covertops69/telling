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
        public Int32 SessionId { get; set; }
        public Int32 GameId { get; set; }
        public string GameName { get; set; }
        public string ImageName { get; set; }
        public DateTime SessionDate { get; set; }
        public string Venue { get; set; }

        public Int32[] PlayerIds { get; set; }
    }
}
