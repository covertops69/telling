using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.ViewModels.Players;

namespace Telling.Core.Models
{
    class SelectedPlayersMessage : MvxMessage
    {
        public ObservableCollection<PlayerViewModel> SelectedPlayers { get; set; }

        public SelectedPlayersMessage(object sender, ObservableCollection<PlayerViewModel> selectedPlayers)
        : base(sender)
        {
            SelectedPlayers = selectedPlayers;
        }
    }
}
