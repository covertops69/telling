using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;

namespace Telling.Droid.Controls
{
    public class UserTileView : ViewGroup
    {
        public UserTileView(Context context) : base(context)
        {
            Initialize(context);
        }

        public UserTileView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context);
        }

        public UserTileView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(context);
        }

        public UserTileView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Initialize(context);
        }

        protected UserTileView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        private void Initialize(Context context)
        {
            var standardMargin = (int)Resources.GetDimension(Resource.Dimension.standard_margin);
            var tileDimension = (int)Resources.GetDimension(Resource.Dimension.player_tile_dimension);

            Background = context.GetDrawable(Resource.Layout.shape_player_tile);

            var layout = new RelativeLayout.LayoutParams(tileDimension, tileDimension);
            layout.SetMargins(standardMargin, standardMargin, standardMargin, standardMargin);
            LayoutParameters = layout;
        }

        public void SetLeftMarginFlush()
        {
            var layout = (RelativeLayout.LayoutParams)LayoutParameters;
            layout.SetMargins(0, layout.TopMargin, layout.RightMargin, layout.BottomMargin);
            LayoutParameters = layout;
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            for (int i = 0; i < ChildCount; i++)
            {
                GetChildAt(i).Layout(l, t, r, b);
            }
        }
    }
}