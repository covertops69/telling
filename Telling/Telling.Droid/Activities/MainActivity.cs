using Android.App;
using Android.OS;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using Telling.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using MvvmCross.Droid.Shared.Caching;
using Android.Views;
using Telling.Droid.Views.Fragments;

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

            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(false);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    base.OnBackPressed();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        #region Fragment LifeCycle

        public override void OnFragmentCreated(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            base.OnFragmentCreated(fragmentInfo, transaction);

            if (!(fragmentInfo.CachedFragment is SessionListingFragment))
            {
                fragmentInfo.AddToBackStack = true;
            }
        }

        public override void OnBeforeFragmentChanging(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            base.OnBeforeFragmentChanging(fragmentInfo, transaction);
            //transaction.SetCustomAnimations(Resource.Animation.enter_from_right, Resource.Animation.exit_to_right);
        }

        #endregion
    }
}