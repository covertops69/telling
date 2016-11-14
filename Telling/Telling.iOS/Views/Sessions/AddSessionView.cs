using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections.ObjectModel;
using Telling.Core.Constants;
using Telling.Core.Models;
using Telling.Core.ViewModels.Sessions;
using Telling.iOS.Controls;
using Telling.iOS.Converters;
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

            var scrollView = new UIScrollView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            Add(scrollView);

            var sessionDateTextField = new TFloatingPicker(new CGRect())
            {
                Placeholder = "When?"
            };
            scrollView.Add(sessionDateTextField);
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
                Placeholder = "What?"
            };
            scrollView.Add(gameTextField);
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
            scrollView.Add(whereTextField);
            bindingSet.Bind(whereTextField).To(vm => vm.Venue).Apply();
            bindingSet.Bind(whereTextField).For("ValidationError").To(vm => vm.ValidationErrors[nameof(AddSessionViewModel.Venue)]).Apply();

            var playerListing = new TTableView();
            scrollView.Add(playerListing);

            var tableSource = new PlayerTableSource(playerListing);
            bindingSet.Bind(tableSource).To(vm => vm.PlayerCollection).Apply();

            var saveButton = new TButtonView("Save");
            scrollView.Add(saveButton);
            bindingSet.Bind(saveButton).To(vm => vm.SaveCommand).Apply();

            View.AddConstraints(new FluentLayout[] {

                scrollView.AtTopOf(View),
                scrollView.WithSameWidth(View),
                scrollView.AtBottomOf(View),

            });

            scrollView.AddConstraints(new FluentLayout[] {

                sessionDateTextField.AtTopOf(scrollView, Constants.Margin),
                sessionDateTextField.AtLeftOf(scrollView, Constants.Margin),
                sessionDateTextField.WithSameWidth(scrollView).Minus(Constants.Margin * 2),

                gameTextField.Below(sessionDateTextField, Constants.Margin),
                gameTextField.AtLeftOf(scrollView, Constants.Margin),
                gameTextField.WithSameWidth(scrollView).Minus(Constants.Margin * 2),

                whereTextField.Below(gameTextField, Constants.Margin),
                whereTextField.AtLeftOf(scrollView, Constants.Margin),
                whereTextField.WithSameWidth(scrollView).Minus(Constants.Margin * 2),

                playerListing.Below(whereTextField, Constants.Margin),
                playerListing.AtLeftOf(scrollView, Constants.Margin),
                playerListing.WithSameWidth(scrollView).Minus(Constants.Margin * 2),
                playerListing.Height().EqualTo(175f),

                saveButton.Below(playerListing, Constants.Margin + 10f),
                saveButton.AtLeftOf(scrollView, Constants.Margin),
                saveButton.WithSameWidth(scrollView).Minus(Constants.Margin * 2),
                saveButton.Height().EqualTo(Constants.ButtonHeight),

                saveButton.AtBottomOf(scrollView, Constants.Margin)

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