using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Managers;
using Telling.Core.Models;
using Telling.Core.ViewModels.Modals;

namespace Telling.Core.ViewModels.Sessions
{
    public class SessionListingViewModel : BaseViewModel
    {
        protected ISessionManager SessionManager { get; }

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

        public SessionListingViewModel(ISessionManager sessionManager)
        {
            SessionManager = sessionManager;
        }

        public async override void Start()
        {
            base.Start();
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                IsBusy = true;

                SessionsCollection = new ObservableCollection<Session>(await SessionManager.GetSessionsAsync());
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
                throw ex;
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
    }
}
