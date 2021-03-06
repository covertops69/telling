﻿using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.StateMachine;

namespace Telling.Core.ViewModels.Modals
{
    public class ModalViewModel : BaseViewModel
    {
        //protected override Trigger StateTrigger
        //{
        //    get
        //    {
        //        return Trigger.Modal;
        //    }
        //}

        private string _exception;
        public string Exception
        {
            get
            {
                return _exception;
            }
            set
            {
                SetProperty(ref _exception, value);
            }
        }

        public void Init(string exceptionMessage)
        {
            Exception = exceptionMessage;
            Title = "Ugh ...";
        }

        IMvxCommand _closeCommand;
        public IMvxCommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new MvxCommand(() =>
                {
                    Close(this);
                }));
            }
        }
    }
}