﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Managers;
using Telling.Core.Models;

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

                //await GetSubscriptionDetailsAsync();
                //await LoadFreeResourcesAsync();

                //if (ProductId != null && PlanType != PlanType.Prepaid && PlanType != PlanType.Unknown)
                //{
                //    var response = await ProductsManager.GetAccountDetailsAsync(ProductId).ConfigureAwait(false);

                //    if (response != null)
                //        IsEligibleToUpgrade = response.RenewalDate < DateTime.Today;
                //}

                //if (!SettingsManager.GetONNET() || Utilities.IsLoggedIn())
                //    await LoadUsageHistoryDetailAsync();
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
                //ShowGenericErrorModalPopup(ex);
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}