using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.StateMachine;

namespace Telling.Core.ViewModels.Modals
{
    public class PickerModalViewModel<TModel> : BaseViewModel
    {

        private ObservableCollection<TModel> _collection;
        public ObservableCollection<TModel> Collection
        {
            get
            {
                return _collection;
            }
            set
            {
                SetProperty(ref _collection, value);
            }
        }

        protected override Trigger StateTrigger
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private async Task LoadAsync()
        {
            try
            {
                IsBusy = true;

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

        //public void Init(string exceptionMessage)
        //{
        //    Exception = exceptionMessage;
        //    Title = "Ugh ...";
        //}

        //IMvxCommand _closeCommand;
        //public IMvxCommand CloseCommand
        //{
        //    get
        //    {
        //        return _closeCommand ?? (_closeCommand = new MvxCommand(() =>
        //        {
        //            Close(this);
        //        }));
        //    }
        //}
    }
}