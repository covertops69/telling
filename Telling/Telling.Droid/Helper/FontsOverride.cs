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
using Android.Graphics;
using Java.Lang;
using MvvmCross.Platform;

namespace Telling.Droid.Helper
{
    public class FontsOverride
    {

        public static void SetDefaultFont(Context context, string staticTypefaceFieldName, string fontAssetName)
        {
            var regular = Typeface.CreateFromAsset(context.Assets, fontAssetName);
            ReplaceFont(staticTypefaceFieldName, regular);
        }

        protected static void ReplaceFont(string staticTypefaceFieldName, Typeface newTypeface)
        {
            try
            {
                var staticField = Class.FromType(typeof(Typeface)).GetDeclaredField(staticTypefaceFieldName);
                staticField.Accessible = true;
                staticField.Set(null, newTypeface);
            }
            catch (NoSuchFieldException e)
            {
                Mvx.Warning(e.Message);
            }
            catch (IllegalAccessException e)
            {
                Mvx.Warning(e.Message);
            }
        }
    }
}