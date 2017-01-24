using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace Telling.Droid.Controls
{
    [Register("telling.android.controls.TImageView")]
    public class TImageView : ImageView
    {
        protected TImageView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public TImageView(Context context)
            : this(context, null)
        {
        }

        public TImageView(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public TImageView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
        }

        protected override void OnDraw(Canvas canvas)
        {
            // clip the boundaries
            float radius = 10f;
            Path path = new Path();
            RectF rect = new RectF(0f, 0f, this.Width, this.Height);

            path.AddRoundRect(rect, radius, radius, Path.Direction.Cw);
            canvas.ClipPath(path);

            base.OnDraw(canvas);
            //canvas.Restore();

            // add border
            path = new Path();
            path.AddRoundRect(rect, radius, radius, Path.Direction.Cw);

            var paint = new Paint();
            paint.AntiAlias = true;
            paint.StrokeWidth = 2f;
            paint.SetStyle(Paint.Style.Stroke);
            paint.Color = global::Android.Graphics.Color.White;

            canvas.DrawPath(path, paint);

            // dispose
            paint.Dispose();
            path.Dispose();
        }        
    }
}