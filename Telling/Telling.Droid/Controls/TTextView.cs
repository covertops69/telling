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
using Android.Util;
using Android.Graphics;

namespace Telling.Droid.Controls
{
    [Register("telling.android.controls.TTextView")]
    public class TTextView : TextView
    {
        protected TTextView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            Initialize();
        }

        public TTextView(Context context)
            : this(context, null)
        {
            Initialize();
        }

        public TTextView(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
            Initialize();
        }

        public TTextView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize();
        }

        private void Initialize()
        {
            SetTextColor(Color.White);
        }
    }
}