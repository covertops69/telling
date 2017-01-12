using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using Telling.Core.ViewModels;

namespace Telling.Droid.Activities
{
    [Activity(
        Label = "Main Activity",
        //Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleInstance,
        Name = "telling.droid.activities.MainActivity"
    )]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
        }
    }
}