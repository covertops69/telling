using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Managers;
using Telling.Core.Models;

namespace Telling.Core.ViewModels.Sessions
{
    public class AddSessionViewModel : BaseValidationViewModel
    {
        protected IGameManager GameManager { get; }

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

        private string _sessionDate;
        public string SessionDate
        {
            get
            {
                return _sessionDate;
            }
            set
            {
                SetProperty(ref _sessionDate, value);
            }
        }

        private Guid _gameId;
        public Guid GameId
        {
            get
            {
                return _gameId;
            }
            set
            {
                SetProperty(ref _gameId, value);
            }
        }

        public AddSessionViewModel(IGameManager gameManager)
        {
            GameManager = gameManager;
            Title = "New";
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

                GamesCollection = new ObservableCollection<Game>(await GameManager.GetGamesAsync());
            }
            //catch (NotConnectedException)
            //{
            //    ShowNotConnectedModalPopup();
            //}
            //catch (WebServiceException wsex)
            //{
            //    ShowWebServiceErrorModalPopup(wsex);
            //}
            catch (Exception ex)
            {
                ShowException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        IMvxAsyncCommand _saveCommand;
        public IMvxAsyncCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new MvxAsyncCommand(async () =>
                {
                    if (!IsBusy)
                    {
                        Close(this);
                    }
                }));
            }
        }
    }
}
