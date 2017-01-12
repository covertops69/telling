using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Platform;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Collections.Generic;
using System.Reflection;

namespace Telling.Droid
{
    public class Setup : MvxAppCompatSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            //typeof(Android.Support.Design.Widget.FloatingActionButton).Assembly,
            //typeof(Android.Support.Design.Widget.NavigationView).Assembly,
            typeof(MvvmCross.Droid.Support.V7.RecyclerView.MvxRecyclerView).Assembly
        };

        //protected override IMvxAndroidViewPresenter CreateViewPresenter()
        //{
        //    var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
        //    Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);

        //    return mvxFragmentsPresenter;
        //}
    }
}
