using FluentValidation.Results;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Managers;
using Telling.Core.Models;
using Telling.Core.Validators;

namespace Telling.Core.ViewModels.Sessions
{
    public class AddSessionViewModel : BaseViewModel
    {
        private SessionValidator _validator;

        protected ISessionManager SessionManager { get; }
        protected IGameManager GameManager { get; }
        protected IPlayerManager PlayerManager { get; }

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

        private ObservableCollection<Player> _playerCollection;
        public ObservableCollection<Player> PlayerCollection
        {
            get
            {
                return _playerCollection;
            }
            set
            {
                SetProperty(ref _playerCollection, value);
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

        private ObservableCollection<Player> _selectedPlayers;
        public ObservableCollection<Player> SelectedPlayers
        {
            get
            {
                return _selectedPlayers;
            }
            set
            {
                SetProperty(ref _selectedPlayers, value);
            }
        }

        private string _venue;
        public string Venue
        {
            get
            {
                return _venue;
            }
            set
            {
                SetProperty(ref _venue, value);
            }
        }

        public AddSessionViewModel(ISessionManager sessionManager, IGameManager gameManager, IPlayerManager playerManager)
        {
            _validator = new SessionValidator();

            SessionManager = sessionManager;
            GameManager = gameManager;
            PlayerManager = playerManager;

            Title = "New";
        }

        public async override void Start()
        {
            base.Start();

            await LoadGamesAsync();
            await LoadPlayerAsync();
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

        private async Task LoadPlayerAsync()
        {
            try
            {
                IsBusy = true;

                PlayerCollection = new ObservableCollection<Player>(await PlayerManager.GetPlayersAsync());
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
                            if (Validate())
                            {
                                IsBusy = true;

                                await SessionManager.CreateSessionAsync(new Session
                                {
                                    GameId = SelectedGame.GameId,
                                    SessionDate = SessionDate,
                                    Venue = Venue
                                });

                                Close(this);
                            }
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
                }));
            }
        }

        public override bool Validate()
        {
            ValidationErrors.Clear();

            _validator = new SessionValidator();

            var results = _validator.Validate(new Session
            {
                GameId = SelectedGame.GameId,
                SessionDate = SessionDate,
                Venue = Venue
            });

            IList<ValidationFailure> failures = results.Errors;

            foreach (var f in failures)
            {
                if (!ValidationErrors.ContainsKey(f.PropertyName))
                {
                    ValidationErrors.Add(f.PropertyName, f.ErrorMessage);
                }
            }

            return results.IsValid;
        }
    }
}
