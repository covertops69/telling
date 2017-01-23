using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace Telling.Droid
{
    [Activity(
        MainLauncher = true,
        Theme = "@style/Theme.Splash",
        Label = "Telling",
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
    }
}
