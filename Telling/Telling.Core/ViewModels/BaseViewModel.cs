using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Stateless;
using System;
using Telling.Core.Helpers;
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

        StateMachine<State, Trigger> _stateMachine;
        protected abstract Trigger StateTrigger { get; }

        public BaseViewModel()
        {
            _stateMachine = Mvx.Resolve<StateMachine<State, Trigger>>();
            UpdateState();

            ValidationErrors = new ObservableDictionary<string, string>();

            var x = RequestedBy;
        }

        void UpdateState()
        {
            try
            {
                _stateMachine.Fire(StateTrigger);
            }
            catch (InvalidOperationException ioex)
            {
                ShowException(ioex);
            }
        }

        public virtual bool Validate()
        {
            return true;
        }

        protected void ShowException(Exception ex)
        {
            ShowViewModel<ModalViewModel>(new { exceptionMessage = ex.Message });
        }

        public void Fire(Trigger trigger)
        {
            _stateMachine.Fire(trigger);
        }
    }
}
