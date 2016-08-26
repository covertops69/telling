using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.Core.Models;
using Telling.Core.ViewModels.Sessions;
using Telling.iOS.Controls;
using UIKit;

namespace Telling.iOS.Views.Cells
{
    [Register("SessionListingItemCell")]
    class SessionListingItemCell : BaseItemCell
    {
        public const string CellIdentifier = "SessionListingItemCell";

        public SessionListingItemCell(IntPtr handle)
            : base(handle)
        {
            CreateLayout();
            InitializeBindings();
        }

        public TLabel Title;

        void CreateLayout()
        {
            Title = new TLabel();
            ContentView.AddSubviews(Title);

            ContentView.AddConstraints(new FluentLayout[] {

                Title.AtTopOf(ContentView),
                Title.AtLeftOf(ContentView),
                //Title.Width().EqualTo(ContentView.Frame.Size.Width)

            });

        }

        void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SessionListingItemCell, Session>();
                set.Bind(Title).To(vm => vm.GameName);//.WithConversion(new StringToNSAttributedStringConverter(), 16.0f);
                //set.Bind(IconImageView).For(c => c.Image).To(vm => vm.ImageName).WithConversion(new StringToUIImageValueConverter());
                //set.Bind(NewImage).For(c => c.Hidden).To(vm => vm.New).WithConversion(new VisibleConverter());
                set.Apply();
            });
        }
    }
}
