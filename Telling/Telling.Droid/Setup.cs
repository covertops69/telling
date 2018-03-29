using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Collections.Generic;
using System.Reflection;
using Telling.Core.Interfaces;
using Telling.Droid.Services;
using MvvmCross.Binding.Bindings.Target.Construction;
using Telling.Core;
using Telling.Droid.Controls;
using Telling.Droid.Bindings;
using Android.Views;

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

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            Mvx.RegisterSingleton<IConnectivityService>(() => new ConnectivityService());
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterCustomBindingFactory<TInputValidation>(Constants.INPUT_VALIDATION_TEXT, input => new InputEditTextTargetBinding(input));
            registry.RegisterCustomBindingFactory<TInputValidation>(Constants.INPUT_VALIDATION_ERROR, input => new InputEditTextErrorTargetBinding(input));

            registry.RegisterCustomBindingFactory<IMenuItem>(Constants.MENU_ITEM_VISIBILITY, input => new MenuItemVisibilityTargetBinding(input));

        }

        //protected override IMvxAndroidViewPresenter CreateViewPresenter()
        //{
        //    var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
        //    Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);

        //    return mvxFragmentsPresenter;
        //}
    }
}
