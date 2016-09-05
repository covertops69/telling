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

            var sessionDateTextField = new TTextField
            {
                Placeholder = "When?"
            };
            Add(sessionDateTextField);
            bindingSet.Bind(sessionDateTextField).To("Format('{0:d MMM yyyy}', SessionDate)").Apply();

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

            var gameTextField = new TTextField
            {
                Placeholder = "What?"
            };
            Add(gameTextField);
            bindingSet.Bind(gameTextField).To(vm => vm.SelectedGame).Apply();

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

            var saveButton = new TButtonView("Save");
            Add(saveButton);
            bindingSet.Bind(saveButton).To(vm => vm.SaveCommand).Apply();

            View.AddConstraints(new FluentLayout[] {

                sessionDateTextField.AtTopOf(View, Constants.Margin),
                sessionDateTextField.AtLeftOf(View, Constants.Margin),
                sessionDateTextField.WithSameWidth(View).Minus(Constants.Margin * 2),

                gameTextField.Below(sessionDateTextField, Constants.Margin),
                gameTextField.AtLeftOf(View, Constants.Margin),
                gameTextField.WithSameWidth(View).Minus(Constants.Margin * 2),

                saveButton.Below(gameTextField, Constants.Margin),
                saveButton.AtLeftOf(View, Constants.Margin),
                saveButton.WithSameWidth(View).Minus(Constants.Margin * 2),
                saveButton.Height().EqualTo(Constants.ButtonHeight)

            });
        }

        public static DateTime NSDateToDateTime(NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }
    }
}