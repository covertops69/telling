using FluentValidation.Results;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Extensions;
using Telling.Core.Helpers;
using Telling.Core.Models;
using Telling.Core.Services;
using Telling.Core.StateMachine;
using Telling.Core.Validation;
using Telling.Core.Validators;
using Telling.Core.ViewModels.Games;
using Telling.Core.ViewModels.Players;

namespace Telling.Core.ViewModels.Sessions
{
    public class AddSessionViewModel : BaseViewModel
    {
        //private SessionValidator _validator;

        protected IValidateRequest _validateRequest;
        protected ISessionService SessionService { get; }
        protected IGameService GameService { get; }
        protected IPlayerService PlayerService { get; }

        private IMvxMessenger _mvxMessenger;
        private MvxSubscriptionToken _messageTokenGame;
        private MvxSubscriptionToken _messageTokenPlayers;

        private ObservableCollection<PlayerViewModel> _selectedPlayers;
        public ObservableCollection<PlayerViewModel> SelectedPlayers
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
                ValidateChange<DateTime, Session, SessionValidator>(value, _validateRequest, nameof(Session.SessionDate));
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
                ValidateChange<Game, Session, SessionValidator>(value, _validateRequest, nameof(Session.Game));
            }
        }

        private Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get
            {
                return _selectedPlayer;
            }
            set
            {
                SetProperty(ref _selectedPlayer, value);
                ValidateChange<Player, Session, SessionValidator>(value, _validateRequest, nameof(Session.Player));
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
                ValidateChange<string, Session, SessionValidator>(value, _validateRequest, nameof(Session.Venue));
            }
        }

        public AddSessionViewModel(ISessionService sessionService, IGameService gameService, IPlayerService playerService, IValidateRequest validateRequest, IMvxMessenger mvxMessenger)
        {
            SessionService = sessionService;
            GameService = gameService;
            PlayerService = playerService;

            _validateRequest = validateRequest;

            _mvxMessenger = mvxMessenger;
            _messageTokenGame = _mvxMessenger.Subscribe<SelectedGameMessage>((obj) =>
            {
                SelectedGame = obj.SelectedGame;
            });

            _messageTokenPlayers = _mvxMessenger.Subscribe<SelectedPlayersMessage>((obj) =>
            {
                SelectedPlayers = obj.SelectedPlayers;
            });

            Title = "New Session";
        }

        //public async override void Start()
        //{
        //    base.Start();

        //    //await LoadGamesAsync();
        //    //await LoadPlayerAsync();
        //}

        //private async Task LoadGamesAsync()
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        var gamesResponse = await GameService.GetGamesAsync();

        //        if (!ProcessResponse(gamesResponse))
        //            return;

        //        GamesCollection = gamesResponse.Result.ToObservableCollection();
        //        SelectedGame = GamesCollection[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowException(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        //private async Task LoadPlayerAsync()
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        var playersResponse = await PlayerService.GetPlayersAsync();

        //        if (!ProcessResponse(playersResponse))
        //            return;

        //        PlayerCollection = playersResponse.Result.ToObservableCollection();
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowException(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

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

                                //var playerIds = PlayerCollection
                                //        .Where(x => x.IsSelected == true)
                                //        .Select(x => x.PlayerId)
                                //        .ToArray<Int32>();

                                var session = new Session
                                {
                                    Venue = Venue,
                                    SessionDate = SessionDate,
                                    Game = SelectedGame
                                };

                                await SessionService.CreateSessionAsync(session);
                                _mvxMessenger.Publish<RefreshRequestMessage>(new RefreshRequestMessage(this));

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

        private MvxCommand _selectGameCommand;
        public MvxCommand SelectGameCommand
        {
            get
            {
                return _selectGameCommand ?? (_selectGameCommand = new MvxCommand(() =>
                {
                    ShowViewModel<GameSelectionViewModel>();
                }));
            }
        }

        private MvxCommand _selectPlayerCommand;
        public MvxCommand SelectPlayerCommand
        {
            get
            {
                return _selectPlayerCommand ?? (_selectPlayerCommand = new MvxCommand(() =>
                {
                    ShowViewModel<PlayerSelectionViewModel>();
                }));
            }
        }

        public bool Validate()
        {
            ValidationErrors.Clear();
            RaisePropertyChanged(() => ValidationErrors);

            var validationResults = _validateRequest.Validate<Session, SessionValidator>(new Session
            {
                Venue = Venue,
                SessionDate = SessionDate,
                Game = SelectedGame
            });

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    if (!ValidationErrors.ContainsKey(error.Key))
                    {
                        ValidationErrors.Add(error.Key, error.Value);
                    }
                }

                RaisePropertyChanged(() => ValidationErrors);

                return false;
            }

            return true;
        }
    }
}
