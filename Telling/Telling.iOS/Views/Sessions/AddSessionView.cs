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
            bindingSet.Bind(Loader).For(b => b.Hidden).To(vm => vm.IsBusy).WithConversion(new LoaderVisibilityConverter()).Apply();
            bindingSet.Bind(this).For(c => c.Title).To(vm => vm.Title).Apply();

            var sessionDate = new TTextField
            {
                Placeholder = "When?"
            };
            Add(sessionDate);
            bindingSet.Bind(sessionDate).To(vm => vm.SessionDate).WithConversion(new StringToDateTimeConverter()).OneWayToSource().Apply();

            /* picker */
            /* ******************************************************* */
            var sessionDatePicker = new UIDatePicker();
            sessionDatePicker.Mode = UIDatePickerMode.Date;

            var sessionDateToolbar = new UIToolbar(new CGRect(0.0f, 0.0f, sessionDatePicker.Frame.Size.Width, 44.0f));

            sessionDate.InputAccessoryView = sessionDateToolbar;
            sessionDate.InputView = sessionDatePicker;

            sessionDateToolbar.Items = new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                {
                    sessionDate.Text = NSDateToDateTime(sessionDatePicker.Date).ToString("dd MMM yyyy");
                    sessionDate.ResignFirstResponder();
                })
            };
            /* ******************************************************* */

            var gameTextField = new TTextField
            {
                Placeholder = "What?"
            };
            Add(gameTextField);

            /* picker */
            /* ******************************************************* */
            var gamePicker = new UIPickerView();
            var gamePickerViewModel = new GamePickerViewModel(gamePicker);
            gamePicker.Model = gamePickerViewModel;
            gamePicker.ShowSelectionIndicator = true;

            bindingSet.Bind(gamePickerViewModel).For(p => p.ItemsSource).To(vm => vm.GamesCollection).Apply();

            var gamePickerToolbar = new UIToolbar(new CGRect(0.0f, 0.0f, gamePicker.Frame.Size.Width, 44.0f));

            gameTextField.InputAccessoryView = gamePickerToolbar;
            gameTextField.InputView = gamePicker;

            gamePickerToolbar.Items = new[]
            {
                new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                {
                    var i = gamePicker.SelectedRowInComponent(0);
                    var selected = ((ObservableCollection<Game>)gamePickerViewModel.ItemsSource)[Convert.ToInt32(i.ToString())];

                    ViewModel.GameId = selected.GameId;
                    gameTextField.Text = selected.Name;

                    gameTextField.ResignFirstResponder();
                })
            };
            /* ******************************************************* */

            var saveButton = new TButton("Save");
            Add(saveButton);
            bindingSet.Bind(saveButton).To(vm => vm.SaveCommand).Apply();

            View.AddConstraints(new FluentLayout[] {

                sessionDate.AtTopOf(View, Constants.Margin),
                sessionDate.AtLeftOf(View, Constants.Margin),
                sessionDate.WithSameWidth(View).Minus(Constants.Margin * 2),

                gameTextField.Below(sessionDate, Constants.Margin),
                gameTextField.AtLeftOf(View, Constants.Margin),
                gameTextField.WithSameWidth(View).Minus(Constants.Margin * 2),

                saveButton.Below(gameTextField, Constants.Margin),
                saveButton.AtLeftOf(View, Constants.Margin),
                saveButton.WithSameWidth(View).Minus(Constants.Margin * 2),

            });
        }

        public static DateTime NSDateToDateTime(NSDate date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }
    }

    public class GamePickerViewModel : MvxPickerViewModel
    {
        public GamePickerViewModel(UIPickerView pickerView) : base(pickerView)
        {
        }

        protected override string RowTitle(nint row, object item)
        {
            return ((Game)item).Name;
        }
    }
}
