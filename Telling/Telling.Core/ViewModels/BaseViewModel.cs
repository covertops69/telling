using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
//using Stateless;
using System;
using Telling.Core.Helpers;
using Telling.Core.Models;
using Telling.Core.StateMachine;
using Telling.Core.ViewModels.Modals;

namespace Telling.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        ObservableDictionary<string, string> _validationErrors;
        public ObservableDictionary<string, string> ValidationErrors
        {
            get { return _validationErrors ?? (_validationErrors = new ObservableDictionary<string, string>()); }
            set { SetProperty(ref _validationErrors, value); }
        }

        public BaseViewModel()
        {
            ValidationErrors = new ObservableDictionary<string, string>();
        }

        public virtual bool Validate()
        {
            return true;
        }

        public bool ProcessResponse<TResp>(BaseResponse<TResp> response)
            where TResp : class
        {
            var isSuccess = response?.ReponseType == ServiceReponseType.Successful;

            if (!isSuccess)
            {
                ShowViewModel<ModalViewModel>(new { exceptionMessage = response?.Message ?? Constants.API_ERROR_NO_MESSAGE });
            }

            return isSuccess;
        }

        protected void ShowException(Exception ex)
        {
            ShowViewModel<ModalViewModel>(new { exceptionMessage = ex.Message });
        }
    }
}
