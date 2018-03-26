using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telling.Core.Extensions;
using Telling.Core.Models;
using Telling.Core.Services;

namespace Telling.Core.ViewModels.Players
{
    public class PlayerSelectionViewModel : BaseViewModel
    {
        protected IPlayerService PlayerService { get; }

        IMvxMessenger _mvxMessenger;

        MvxAsyncCommand<Player> _itemSelectedCommand;
        public MvxAsyncCommand<Player> ItemSelectedCommand =>
            _itemSelectedCommand ?? (_itemSelectedCommand = new MvxAsyncCommand<Player>(SelectedPlayer));

        private ObservableCollection<Player> _gamesCollection;
        public ObservableCollection<Player> PlayersCollection
        {
            get
            {
                return _gamesCollection;
            }
            set
            {
                SetProperty(ref _gamesCollection, value);
            }
        }

        //private Player _gameSelected;
        //public Player PlayerSelected
        //{
        //    get
        //    {
        //        return _gameSelected;
        //    }
        //    set
        //    {
        //        SetProperty(ref _gameSelected, value);
        //    }
        //}

        public PlayerSelectionViewModel(IPlayerService gameService, IMvxMessenger mvxMessenger)
        {
            PlayerService = gameService;
            _mvxMessenger = mvxMessenger;

            Title = "Players";
        }

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

                var gamesResponse = await PlayerService.GetPlayersAsync();

                if (!ProcessResponse(gamesResponse))
                    return;

                PlayersCollection = gamesResponse.Result.ToObservableCollection();
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

        async Task SelectedPlayer(Player game)
        {
            _mvxMessenger.Publish<SelectedPlayerMessage>(new SelectedPlayerMessage(this, game));
            Close(this);
        }
    }
}
