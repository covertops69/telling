using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Managers;

namespace Telling.Core.ViewModels.Sessions
{
    public class SessionListingViewModel : BaseViewModel
    {
        protected ISessionManager SessionManager { get; }

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

                var result = await SessionManager.GetSessionsAsync();
                var t = true;

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
