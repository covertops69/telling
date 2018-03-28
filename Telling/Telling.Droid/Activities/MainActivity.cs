using Android.App;
using Android.OS;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using Telling.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Views;
using Telling.Droid.Views.Fragments;
using Android.Widget;

namespace Telling.Droid.Activities
{
    [Activity(
        Label = "Telling",
        Theme = "@style/MyTheme",
        LaunchMode = LaunchMode.SingleTask,
        Name = "telling.droid.activities.MainActivity",
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden,
        WindowSoftInputMode = SoftInput.AdjustPan
    )]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
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
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}