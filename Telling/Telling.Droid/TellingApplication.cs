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
using Telling.Droid.Helper;

namespace Telling.Droid
{
    [Application(Name = "telling.droid.TellingApplication")]
    public class TellingApplication : Application
    {
        public TellingApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            FontsOverride.SetDefaultFont(this, "MONOSPACE", "fonts/BrookeS8.ttf");
        }
    }
}