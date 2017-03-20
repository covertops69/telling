using Cirrious.FluentLayouts.Touch;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using System;
using Telling.Core;
using Telling.Core.ViewModels.Sessions;
using Telling.iOS.Controls;
using Telling.iOS.Helpers;
using Telling.iOS.TableSources;
using UIKit;

namespace Telling.iOS.Views.Sessions
{
    class AddSessionView : BaseViewController<AddSessionViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<AddSessionView, AddSessionViewModel>();
            bindingSet = BindLoader(bindingSet);

            var sessionDateTextField = new TFloatingPicker(new CGRect())
            {
                Placeholder = "When?"
            };
            this.View.Add(sessionDateTextField);
            bindingSet.Bind(sessionDateTextField).To("Format('{0:d MMM yyyy}', SessionDate)").Apply();
            bindingSet.Bind(sessionDateTextField).For("ValidationError").To(vm => vm.ValidationErrors[nameof(AddSessionViewModel.SessionDate)]).Apply();

            /* picker */
            /* ******************************************************* */
            var sessionDatePicker = new UIDatePicker();
            sessionDatePicker.Mode = UIDatePickerMode.Date;

            var sessionDateToolbar = new UIToolbar(new CGRect(0.0f, 0.0f, sessionDatePicker.Frame.Size.Width, 44.0f));

            sessionDateTextField.InputAccessoryView = sessionDateToolbar;
            sessionDateTextField.InputView = sessionDatePicker;

            sessionDateToolbar.Items = new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                {
                    sessionDateTextField.ResignFirstResponder();
                })
            };

            bindingSet.Bind(sessionDatePicker).To(vm => vm.SessionDate).Apply();
            /* ******************************************************* */

            var gameTextField = new TFloatingTextField(new CGRect())
            {
                Placeholder = "What?",
                Editable = false
            };

            this.View.Add(gameTextField);
            bindingSet.Bind(gameTextField).To(vm => vm.SelectedGame).Apply();
            bindingSet.Bind(gameTextField).For("ValidationError").To(vm => vm.ValidationErrors[nameof(AddSessionViewModel.SelectedGame)]).Apply();

            /* picker */
            /* ******************************************************* */
            var gamePicker = new UIPickerView();
            var gamePickerViewModel = new MvxPickerViewModel(gamePicker);
            gamePicker.Model = gamePickerViewModel;

            var gamePickerToolbar = new UIToolbar(new CGRect(0.0f, 0.0f, gamePicker.Frame.Size.Width, 44.0f));

            gameTextField.InputAccessoryView = gamePickerToolbar;
            gameTextField.InputView = gamePicker;

            gamePickerToolbar.Items = new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                {
                    gameTextField.ResignFirstResponder();
                })
            };

            bindingSet.Bind(gamePickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedGame).Apply();
            bindingSet.Bind(gamePickerViewModel).For(p => p.ItemsSource).To(vm => vm.GamesCollection).Apply();
            /* ******************************************************* */

            var whereTextField = new TFloatingTextField(new CGRect())
            {
                Placeholder = "Where?"
            };
            this.View.Add(whereTextField);
            bindingSet.Bind(whereTextField).To(vm => vm.Venue).Apply();
            bindingSet.Bind(whereTextField).For("ValidationError").To(vm => vm.ValidationErrors[nameof(AddSessionViewModel.Venue)]).Apply();

            var playerListing = new TTableView
            {
                AllowsSelection = false
            };
            this.View.Add(playerListing);

            var tableSource = new PlayerTableSource(playerListing);
            bindingSet.Bind(tableSource).To(vm => vm.PlayerCollection).Apply();

            var saveButton = new TButtonView("Save");
            this.View.Add(saveButton);
            bindingSet.Bind(saveButton).To(vm => vm.SaveCommand).Apply();

            var gradientTopView = new TGradientView(ColorPalette.Carnelian, ColorPalette.Carnelian.ColorWithAlpha(0));
            this.View.Add(gradientTopView);

            var gradientBottom = new TGradientView(ColorPalette.Carnelian.ColorWithAlpha(0), ColorPalette.Carnelian);
            this.View.Add(gradientBottom);

            this.View.AddConstraints(new FluentLayout[] {

                sessionDateTextField.AtTopOf(this.View, Constants.MARGIN),
                sessionDateTextField.AtLeftOf(this.View, Constants.MARGIN),
                sessionDateTextField.WithSameWidth(this.View).Minus(Constants.MARGIN * 2),

                gameTextField.Below(sessionDateTextField, Constants.MARGIN),
                gameTextField.AtLeftOf(this.View, Constants.MARGIN),
                gameTextField.WithSameWidth(this.View).Minus(Constants.MARGIN * 2),

                whereTextField.Below(gameTextField, Constants.MARGIN),
                whereTextField.AtLeftOf(this.View, Constants.MARGIN),
                whereTextField.WithSameWidth(this.View).Minus(Constants.MARGIN * 2),

                playerListing.Below(whereTextField, Constants.MARGIN),
                playerListing.AtLeftOf(this.View, Constants.MARGIN),
                playerListing.WithSameWidth(this.View).Minus(Constants.MARGIN * 2),
                playerListing.Above(saveButton, Constants.MARGIN),

                saveButton.AtBottomOf(this.View, Constants.MARGIN),
                saveButton.Height().EqualTo(Constants.BUTTON_HEIGHT),
                saveButton.AtLeftOf(this.View, Constants.MARGIN),
                saveButton.WithSameWidth(this.View).Minus(Constants.MARGIN * 2),

                gradientTopView.Below(whereTextField, Constants.MARGIN),
                gradientTopView.AtLeftOf(this.View, Constants.MARGIN),
                gradientTopView.WithSameWidth(this.View).Minus(Constants.MARGIN * 2),
                gradientTopView.Height().EqualTo(10f),

                gradientBottom.Above(saveButton, Constants.MARGIN),
                gradientBottom.AtLeftOf(this.View, Constants.MARGIN),
                gradientBottom.WithSameWidth(this.View).Minus(Constants.MARGIN * 2),
                gradientBottom.Height().EqualTo(10f),

            });

            playerListing.Source = tableSource;
            playerListing.ReloadData();
        }

        public static DateTime NSDateToDateTime(NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }
    }
}