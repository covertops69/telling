using Android.Views;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Telling.Core.Validation;
using Telling.Core.ViewModels.Players;
using Telling.Droid.Controls;

namespace Telling.Droid.Bindings
{
    public class SelectedPlayersCollectionTargetBinding : MvxAndroidTargetBinding<RelativeLayout, ObservableCollection<PlayerViewModel>>
    {
        public SelectedPlayersCollectionTargetBinding(RelativeLayout layout)
            : base(layout)
        {
        }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        protected override void SetValueImpl(RelativeLayout layout, ObservableCollection<PlayerViewModel> selectedPlayers)
        {
            UserTileView previousTile = null;

            foreach (PlayerViewModel p in selectedPlayers.Where(p => p.IsSelected == true))
            {
                var tile = new UserTileView(layout.Context)
                {
                    Id = View.GenerateViewId()
                };

                if (previousTile != null)
                {
                    var layoutParams = (RelativeLayout.LayoutParams)tile.LayoutParameters;
                    layoutParams.AddRule(LayoutRules.RightOf, previousTile.Id);

                    tile.LayoutParameters = layoutParams;
                    tile.SetLeftMarginFlush();
                }

                layout.AddView(tile);

                previousTile = tile;
            }
        }
    }
}