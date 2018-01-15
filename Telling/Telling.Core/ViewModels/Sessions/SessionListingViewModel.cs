using MvvmCross.Core.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telling.Core.Extensions;
using Telling.Core.Models;
using Telling.Core.Services;
using Telling.Core.ViewModels.Games;
using Telling.Core.ViewModels.Modals;

namespace Telling.Core.ViewModels.Sessions
{
    public class SessionListingViewModel : BaseViewModel
    {
        protected ISessionService SessionService { get; }

        private ObservableCollection<Session> _sessionsCollection;
        public ObservableCollection<Session> SessionsCollection
        {
            get
            {
                return _sessionsCollection;
            }
            set
            {
                SetProperty(ref _sessionsCollection, value);
            }
        }

        public SessionListingViewModel(ISessionService sessionService)
        {
            SessionService = sessionService;
            Title = "Sessions";
        }

        public async override void Start()
        {
            base.Start();
            await LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    var sessionsResponse = await SessionService.GetSessionsAsync();

                    if (!ProcessResponse(sessionsResponse))
                        return;

                    SessionsCollection = sessionsResponse.Result.ToObservableCollection();
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

        private MvxCommand<Session> _navigateCommand;
        public MvxCommand<Session> NavigateCommand
        {
            get
            {
                return _navigateCommand ?? (_navigateCommand = new MvxCommand<Session>(menuItem =>
                {
                    ShowViewModel<ModalViewModel>();
                }));
            }
        }

        IMvxAsyncCommand _refreshCommand;
        public IMvxAsyncCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new MvxAsyncCommand(async () =>
                {
                    if (!IsBusy)
                    {
                        await LoadDataAsync();
                    }
                }));
            }
        }

        private MvxCommand _navigateToAddCommand;
        public MvxCommand NavigateToAddCommand
        {
            get
            {
                return _navigateToAddCommand ?? (_navigateToAddCommand = new MvxCommand(() =>
                {
                    ShowViewModel<AddSessionViewModel>();
                }));
            }
        }
    }
}