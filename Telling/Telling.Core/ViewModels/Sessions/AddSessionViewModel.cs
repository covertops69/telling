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
        private MvxSubscriptionToken _messageToken;

        //private ObservableCollection<Game> _gamesCollection;
        //public ObservableCollection<Game> GamesCollection
        //{
        //    get
        //    {
        //        return _gamesCollection;
        //    }
        //    set
        //    {
        //        SetProperty(ref _gamesCollection, value);
        //    }
        //}

        //private ObservableCollection<Player> _playerCollection;
        //public ObservableCollection<Player> PlayerCollection
        //{
        //    get
        //    {
        //        return _playerCollection;
        //    }
        //    set
        //    {
        //        SetProperty(ref _playerCollection, value);
        //    }
        //}

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
            _messageToken = _mvxMessenger.Subscribe<SelectedGameMessage>(OnGameSelected);

            Title = "New Session";
        }

        private void OnGameSelected(SelectedGameMessage obj)
        {
            SelectedGame = obj.SelectedGame;
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

                                //var session = new Session
                                //{
                                //    GameId = SelectedGame.GameId,
                                //    SessionDate = SessionDate,
                                //    Venue = Venue,
                                //};

                                //await SessionService.CreateSessionAsync(session);
                                //_mvxMessenger.Publish<RefreshRequestMessage>(new RefreshRequestMessage(this));

                                //Close(this);
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

            return false;

            //ValidationErrors.Clear();

            //var model = new Session
            //{
            //    SessionDate = SessionDate,
            //    Venue = Venue
            //};

            //if (SelectedGame != null)
            //    model.GameId = SelectedGame.GameId;

            //var validationResults = _validateRequest.Validate<Session, SessionValidator>(model);

            //if (validationResults.Errors.Count == 0)
            //    validationResults.IsValid = true;

            //foreach (var error in validationResults.Errors)
            //    if (!ValidationErrors.ContainsKey(error.Key))
            //        ValidationErrors.Add(error.Key, error.Value);

            //// TODO :: Remove once we know why this isn't happening automatically
            //RaisePropertyChanged(() => ValidationErrors);

            //if (!validationResults.IsValid || ValidationErrors.Count > 0)
            //{
            //    return false;
            //}

            //return true;
        }
    }
}
