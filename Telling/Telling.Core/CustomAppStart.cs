using Cheesebaron.MvxPlugins.Settings.Interfaces;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.ViewModels.Sessions;

namespace Telling.Core
{
    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            var settings = Mvx.Resolve<ISettings>();
            ShowViewModel<SessionListingViewModel>();
        }
    }
}
