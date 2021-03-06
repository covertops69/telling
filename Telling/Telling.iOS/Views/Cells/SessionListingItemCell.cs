﻿using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using System;
using Telling.Core;
using Telling.Core.Models;
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

        public TLabelView Title, SubTitle;
        public TView Seperator, CenterRuler;
        public TCorneredImageView Icon;

        void CreateLayout()
        {
            Icon = new TCorneredImageView(UIImage.FromBundle("Images/missing.png"));
            ContentView.AddSubviews(Icon);

            Title = new TLabelView();
            ContentView.AddSubviews(Title);

            SubTitle = new TLabelView();
            UIFont font = SubTitle.Font;
            SubTitle.Font = font.WithSize(10f);
            ContentView.AddSubviews(SubTitle);

            Seperator = TView.MakeSeperator();
            ContentView.AddSubviews(Seperator);

            CenterRuler = TView.MakeSeperator(UIColor.Clear);
            ContentView.AddSubviews(CenterRuler);

            ContentView.AddConstraints(new FluentLayout[] {

                CenterRuler.WithSameCenterY(ContentView),

                Icon.AtLeftOf(ContentView, Constants.MARGIN),
                Icon.WithSameCenterY(ContentView),
                Icon.Height().EqualTo(40f),
                Icon.Width().EqualTo(40f),

                Title.Above(CenterRuler),
                Title.ToRightOf(Icon, Constants.MARGIN),
                //Title.Width().EqualTo(ContentView.Frame.Size.Width)

                SubTitle.Below(CenterRuler),
                SubTitle.ToRightOf(Icon, Constants.MARGIN),

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
                set.Bind(Icon).For(c => c.Image).To(vm => vm.ImageName).WithConversion(new StringToImageConverter());
                //set.Bind(IconImageView).For(c => c.Image).To(vm => vm.ImageName).WithConversion(new StringToUIImageValueConverter());
                //set.Bind(NewImage).For(c => c.Hidden).To(vm => vm.New).WithConversion(new VisibleConverter());
                set.Apply();
            });
        }
    }
}