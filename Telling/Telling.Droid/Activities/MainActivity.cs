using Android.App;
using Android.OS;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using Telling.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using MvvmCross.Droid.Shared.Caching;

namespace Telling.Droid.Activities
{
    [Activity(
        Label = "Telling",
        Theme = "@style/MyTheme",
        LaunchMode = LaunchMode.SingleTask,
        Name = "telling.droid.activities.MainActivity"
    )]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //SupportActionBar.Title = "Sessions";
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        #region Fragment LifeCycle

        public override void OnFragmentCreated(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            base.OnFragmentCreated(fragmentInfo, transaction);
            fragmentInfo.AddToBackStack = true;
        }

        public override void OnBeforeFragmentChanging(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            base.OnBeforeFragmentChanging(fragmentInfo, transaction);
        }

        #endregion
    }
}