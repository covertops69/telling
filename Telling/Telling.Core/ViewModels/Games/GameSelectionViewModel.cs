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

        public GameSelectionViewModel(IGameService gameService)
        {
            GameService = gameService;

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
    }
}
