using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    class SelectedPlayerMessage : MvxMessage
    {
        public Player SelectedPlayer { get; set; }

        public SelectedPlayerMessage(object sender, Player selectedPlayer)
        : base(sender)
        {
            SelectedPlayer = selectedPlayer;
        }
    }
}
