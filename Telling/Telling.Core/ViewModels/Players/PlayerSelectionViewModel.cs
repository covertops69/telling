﻿using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Telling.Core.Extensions;
using Telling.Core.Models;
using Telling.Core.Services;

namespace Telling.Core.ViewModels.Players
{
    public class PlayerSelectionChangedEventArgs
    {
        public bool HasPlayers { get; set; }
    }

    public class PlayerSelectionViewModel : BaseViewModel
    {
        protected IPlayerService PlayerService { get; }

        IMvxMessenger _mvxMessenger;

        MvxAsyncCommand<PlayerViewModel> _itemSelectedCommand;
        public MvxAsyncCommand<PlayerViewModel> ItemSelectedCommand =>
            _itemSelectedCommand ?? (_itemSelectedCommand = new MvxAsyncCommand<PlayerViewModel>(SelectedPlayer));

        private ObservableCollection<PlayerViewModel> _playersCollection;
        public ObservableCollection<PlayerViewModel> PlayersCollection
        {
            get
            {
                return _playersCollection;
            }
            set
            {
                SetProperty(ref _playersCollection, value);
            }
        }

        //private bool _isPlayerSelected;
        //public bool IsPlayerSelected
        //{
        //    get
        //    {
        //        return _isPlayerSelected;
        //    }
        //    set
        //    {
        //        SetProperty(ref _isPlayerSelected, value);
        //    }
        //}

        public PlayerSelectionViewModel(IPlayerService playerService, IMvxMessenger mvxMessenger)
        {
            PlayerService = playerService;
            _mvxMessenger = mvxMessenger;

            Title = "Players";
        }

        public event EventHandler<PlayerSelectionChangedEventArgs> PlayerSelectionChanged;

        public async override void Start()
        {
            base.Start();
            await LoadPlayersAsync();
        }

        private async Task LoadPlayersAsync()
        {
            try
            {
                IsBusy = true;

                var playersResponse = await PlayerService.GetPlayersAsync();

                if (!ProcessResponse(playersResponse))
                    return;

                PlayersCollection = playersResponse.Result.ToObservableCollection();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void SelectedPlayers()
        {
            _mvxMessenger.Publish<SelectedPlayerMessage>(new SelectedPlayerMessage(this, PlayersCollection));
        }

        async Task SelectedPlayer(PlayerViewModel player)
        {
            player.IsSelected = !player.IsSelected;

            if (PlayersCollection.Where(p => p.IsSelected).Count() > 0)
            {
                PlayerSelectionChanged?.Invoke(this, new PlayerSelectionChangedEventArgs { HasPlayers = true });
                Title = string.Format("{0} selected", PlayersCollection.Where(p => p.IsSelected).Count());
            }
            else
            {
                PlayerSelectionChanged?.Invoke(this, new PlayerSelectionChangedEventArgs { HasPlayers = false });
                Title = "Players";
            }

            //player.Sel
            //_mvxMessenger.Publish<SelectedPlayerMessage>(new SelectedPlayerMessage(this, game));
            //Close(this);
        }
    }
}
