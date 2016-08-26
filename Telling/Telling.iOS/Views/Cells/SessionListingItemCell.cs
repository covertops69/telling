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
using Telling.iOS.Converters;
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

        public TLabel Title, SubTitle;
        public TView Seperator, CenterRuler;

        void CreateLayout()
        {
            Title = new TLabel();
            ContentView.AddSubviews(Title);

            SubTitle = new TLabel();
            UIFont font = SubTitle.Font;
            SubTitle.Font = font.WithSize(10f);
            ContentView.AddSubviews(SubTitle);

            Seperator = TView.MakeSeperator();
            ContentView.AddSubviews(Seperator);

            CenterRuler = TView.MakeSeperator(UIColor.Clear);
            ContentView.AddSubviews(CenterRuler);

            ContentView.AddConstraints(new FluentLayout[] {

                CenterRuler.WithSameCenterY(ContentView),

                Title.Above(CenterRuler),
                Title.AtLeftOf(ContentView, 15f),
                //Title.Width().EqualTo(ContentView.Frame.Size.Width)

                SubTitle.Below(CenterRuler),
                SubTitle.AtLeftOf(ContentView, 15f),

                Seperator.AtBottomOf(ContentView),
                Seperator.Height().EqualTo(1f),
                Seperator.WithSameWidth(ContentView),

            });
        }

        void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SessionListingItemCell, Session>();
                set.Bind(Title).To(vm => vm.GameName);//.WithConversion(new StringToNSAttributedStringConverter(), 16.0f);
                set.Bind(SubTitle).To(vm => vm.SessionDate).WithConversion(new DateTimeToStringConverter());
                //set.Bind(IconImageView).For(c => c.Image).To(vm => vm.ImageName).WithConversion(new StringToUIImageValueConverter());
                //set.Bind(NewImage).For(c => c.Hidden).To(vm => vm.New).WithConversion(new VisibleConverter());
                set.Apply();
            });
        }
    }
}