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
        protected ISessionManager SessionManager { get; }
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

        private DateTime _sessionDate = DateTime.Now;
        public DateTime SessionDate
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

        private Game _selectedGame;
        public Game SelectedGame
        {
            get
            {
                return _selectedGame;
            }
            set
            {
                SetProperty(ref _selectedGame, value);
            }
        }

        public AddSessionViewModel(ISessionManager sessionManager, IGameManager gameManager)
        {
            SessionManager = sessionManager;
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
                SelectedGame = GamesCollection[0];
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
                        try
                        {
                            IsBusy = true;

                            await SessionManager.CreateSessionAsync(new Session
                            {
                                GameId = SelectedGame.GameId,
                                SessionDate = SessionDate
                            });

                            Close(this);
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
                }));
            }
        }
    }
}
