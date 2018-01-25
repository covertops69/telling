using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telling.Core.Extensions;
using Telling.Core.Models;
using Telling.Core.Services;

namespace Telling.Core.ViewModels.Games
{
    public class GameSelectionViewModel : BaseViewModel
    {
        protected IGameService GameService { get; }

        IMvxMessenger _mvxMessenger;

        MvxAsyncCommand<Game> _itemSelectedCommand;
        public MvxAsyncCommand<Game> ItemSelectedCommand =>
            _itemSelectedCommand ?? (_itemSelectedCommand = new MvxAsyncCommand<Game>(SelectedGame));

        private ObservableCollection<Game> _gamesCollection;
        public ObservableCollection<Game> GamesCollection
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

        //private Game _gameSelected;
        //public Game GameSelected
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

        public GameSelectionViewModel(IGameService gameService, IMvxMessenger mvxMessenger)
        {
            GameService = gameService;
            _mvxMessenger = mvxMessenger;

            Title = "Games";
        }

        public async override void Start()
        {
            base.Start();
            await LoadGamesAsync();
        }

        private async Task LoadGamesAsync()
        {
            try
            {
                IsBusy = true;

                var gamesResponse = await GameService.GetGamesAsync();

                if (!ProcessResponse(gamesResponse))
                    return;

                GamesCollection = gamesResponse.Result.ToObservableCollection();
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

        async Task SelectedGame(Game game)
        {
            _mvxMessenger.Publish<SelectedGameMessage>(new SelectedGameMessage(this, game));
            Close(this);
        }
    }
}
