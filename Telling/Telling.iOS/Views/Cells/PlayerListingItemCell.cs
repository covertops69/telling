using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using System;
using Telling.Core.Models;
using Telling.iOS.Controls;

namespace Telling.iOS.Views.Cells
{
    [Register("PlayerListingItemCell")]
    class PlayerListingItemCell : BaseItemCell
    {
        public const string CellIdentifier = "PlayerListingItemCell";

        public PlayerListingItemCell(IntPtr handle)
            : base(handle)
        {
            CreateLayout();
            InitializeBindings();
        }

        public TLabelView NameLabel;
        public TCheckbox CheckBox;

        void CreateLayout()
        {
            NameLabel = new TLabelView();
            ContentView.Add(NameLabel);

            CheckBox = new TCheckbox();
            ContentView.Add(CheckBox);

            ContentView.AddConstraints(new FluentLayout[] {

                NameLabel.WithSameCenterY(ContentView),
                NameLabel.AtLeftOf(ContentView),

                CheckBox.WithSameCenterY(ContentView),
                CheckBox.AtRightOf(ContentView),

            });
        }

        void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<PlayerListingItemCell, Player>();
                set.Bind(NameLabel).To(vm => vm.Name);
                set.Bind(CheckBox).For("CheckedStatus").To(vm => vm.IsSelected);
                set.Apply();
            });
        }
    }
}