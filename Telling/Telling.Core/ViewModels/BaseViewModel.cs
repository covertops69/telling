using MvvmCross.Core.ViewModels;
//using MvvmValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Extensions;
using Telling.Core.Helpers;
using Telling.Core.Models;
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

        protected void ShowException(Exception ex)
        {
            ShowViewModel<ModalViewModel>(new { exceptionMessage = ex.Message });
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
    }
}
