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

        public TLabelView NameLabel;//;, SubTitle;
        //public TView Seperator, CenterRuler;
        public TCheckbox CheckBox;

        void CreateLayout()
        {
            //Icon = new TCorneredImageView(UIImage.FromBundle("Images/missing.png"));
            //ContentView.AddSubviews(Icon);

            NameLabel = new TLabelView();
            ContentView.Add(NameLabel);

            CheckBox = new TCheckbox();
            ContentView.Add(CheckBox);

            //SubTitle = new TLabelView();
            //UIFont font = SubTitle.Font;
            //SubTitle.Font = font.WithSize(10f);
            //ContentView.Add(SubTitle);

            //Seperator = TView.MakeSeperator();
            //ContentView.Add(Seperator);

            //CenterRuler = TView.MakeSeperator(UIColor.Clear);
            //ContentView.Add(CenterRuler);

            ContentView.AddConstraints(new FluentLayout[] {

                NameLabel.WithSameCenterY(ContentView),
                NameLabel.AtLeftOf(ContentView),

                CheckBox.WithSameCenterY(ContentView),
                CheckBox.AtRightOf(ContentView),

                //CenterRuler.WithSameCenterY(ContentView),

                //Icon.AtLeftOf(ContentView, Constants.Margin),
                //Icon.WithSameCenterY(ContentView),
                //Icon.Height().EqualTo(40f),
                //Icon.Width().EqualTo(40f),

                //NameLabel.Above(CenterRuler),
                //NameLabel.AtLeftOf(ContentView, Constants.Margin),
                //Title.Width().EqualTo(ContentView.Frame.Size.Width)

                //SubTitle.Below(CenterRuler),
                //SubTitle.ToRightOf(Icon, Constants.Margin),

                //Seperator.AtBottomOf(ContentView),
                //Seperator.Height().EqualTo(1f),
                //Seperator.WithSameWidth(ContentView),

            });
        }

        void InitializeBindings()
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<PlayerListingItemCell, Player>();
                set.Bind(NameLabel).To(vm => vm.Name);//.WithConversion(new StringToNSAttributedStringConverter(), 16.0f);
                //set.Bind(SubTitle).To(vm => vm.PlayerDate).WithConversion(new DateTimeToStringConverter());
                //set.Bind(Icon).For(c => c.Image).To(vm => vm.ImageName).WithConversion(new StringToImageConverter());
                //set.Bind(IconImageView).For(c => c.Image).To(vm => vm.ImageName).WithConversion(new StringToUIImageValueConverter());
                //set.Bind(NewImage).For(c => c.Hidden).To(vm => vm.New).WithConversion(new VisibleConverter());
                set.Apply();
            });
        }
    }
}