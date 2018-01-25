using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    class SelectedGameMessage : MvxMessage
    {
        public Game SelectedGame { get; set; }

        public SelectedGameMessage(object sender, Game selectedGame)
        : base(sender)
        {
            SelectedGame = selectedGame;
        }
    }
}
