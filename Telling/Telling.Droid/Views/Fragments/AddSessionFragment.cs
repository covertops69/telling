using Android.Runtime;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Sessions;
using Android.Views;
using Android.OS;
using Telling.Droid.Controls;
using System;
using MvvmCross.Binding.BindingContext;
using Telling.Core.Converters;
using Android.Views.InputMethods;
using Telling.Core;
using MvvmCross.Droid.Views.Attributes;
using Refractored.Fab;
using Android.Widget;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    [Register("telling.droid.views.fragments.AddSessionFragment")]

    public class AddSessionFragment : BaseFragment<AddSessionViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_addsession;

        TInputValidation _sessionVenueInput, _sessionDateInput, _sessionGameInput;//, _sessionPlayerInput;
        FloatingActionButton _floatingActionButton;
        RelativeLayout _selectedPlayersLayout;

        EventHandler DateInputTap()
            => delegate
            {
                var frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    ViewModel.SessionDate = time;
                },
                ViewModel.SessionDate);
                frag.Show(Activity.FragmentManager, DatePickerFragment.TAG);

                var inputMethodManager = (InputMethodManager)Activity.GetSystemService(Android.Content.Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, 0);
            };

        EventHandler<View.FocusChangeEventArgs> DateInputFocusChange()
            => (sender, args) =>
            {
                if (args.HasFocus)
                {
                    var frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                    {
                        ViewModel.SessionDate = time;
                    },
                            ViewModel.SessionDate);
                    frag.Show(Activity.FragmentManager, DatePickerFragment.TAG);

                    var inputMethodManager = (InputMethodManager)Activity.GetSystemService(Android.Content.Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(Activity.CurrentFocus.WindowToken, 0);
                }
            };

        EventHandler GameInputTap()
            => delegate
            {
                ViewModel.SelectGameCommand.Execute();

                var inputMethodManager = (InputMethodManager)Activity.GetSystemService(Android.Content.Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(Activity.CurrentFocus?.WindowToken, 0);
            };

        EventHandler<View.FocusChangeEventArgs> GameInputFocusChange()
            => (sender, args) =>
            {
                if (args.HasFocus)
                {
                    ViewModel.SelectGameCommand.Execute();

                    var inputMethodManager = (InputMethodManager)Activity.GetSystemService(Android.Content.Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(Activity.CurrentFocus?.WindowToken, 0);
                }
            };

        EventHandler PlayerInputTap()
            => delegate
            {
                ViewModel.SelectPlayerCommand.Execute();

                var inputMethodManager = (InputMethodManager)Activity.GetSystemService(Android.Content.Context.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(Activity.CurrentFocus?.WindowToken, 0);
            };

        EventHandler<View.FocusChangeEventArgs> PlayerInputFocusChange()
            => (sender, args) =>
            {
                if (args.HasFocus)
                {
                    ViewModel.SelectPlayerCommand.Execute();

                    var inputMethodManager = (InputMethodManager)Activity.GetSystemService(Android.Content.Context.InputMethodService);
                    inputMethodManager.HideSoftInputFromWindow(Activity.CurrentFocus?.WindowToken, 0);
                }
            };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _sessionVenueInput = view.FindViewById<TInputValidation>(Resource.Id.session_venue);
            _sessionDateInput = view.FindViewById<TInputValidation>(Resource.Id.session_date);
            _sessionGameInput = view.FindViewById<TInputValidation>(Resource.Id.session_game);
            //_sessionPlayerInput = view.FindViewById<TInputValidation>(Resource.Id.session_player);
            _selectedPlayersLayout = view.FindViewById<RelativeLayout>(Resource.Id.players_selected);

            _floatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab);

            _sessionDateInput.EditTextControl.SetCursorVisible(false);
            _sessionDateInput.EditTextControl.ShowSoftInputOnFocus = false;

            _sessionGameInput.EditTextControl.SetCursorVisible(false);
            _sessionGameInput.EditTextControl.ShowSoftInputOnFocus = false;

            //_sessionPlayerInput.EditTextControl.SetCursorVisible(false);
            //_sessionPlayerInput.EditTextControl.ShowSoftInputOnFocus = false;

            Bind();

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            _sessionDateInput.EditTextControl.Click += DateInputTap();
            _sessionDateInput.EditTextControl.FocusChange += DateInputFocusChange();

            _sessionGameInput.EditTextControl.Click += GameInputTap();
            _sessionGameInput.EditTextControl.FocusChange += GameInputFocusChange();

            //_sessionPlayerInput.EditTextControl.Click += PlayerInputTap();
            //_sessionPlayerInput.EditTextControl.FocusChange += PlayerInputFocusChange();
        }

        public override void OnPause()
        {
            base.OnPause();

            _sessionDateInput.EditTextControl.Click -= DateInputTap();
            _sessionDateInput.EditTextControl.FocusChange -= DateInputFocusChange();

            _sessionGameInput.EditTextControl.Click -= GameInputTap();
            _sessionGameInput.EditTextControl.FocusChange -= GameInputFocusChange();

            //_sessionPlayerInput.EditTextControl.Click -= PlayerInputTap();
           // _sessionPlayerInput.EditTextControl.FocusChange -= PlayerInputFocusChange();
        }

        void Bind()
        {
            var bindingSet = this.CreateBindingSet<AddSessionFragment, AddSessionViewModel>();

            bindingSet.Bind(_sessionVenueInput).For(Constants.INPUT_VALIDATION_TEXT).To(vm => vm.Venue);
            bindingSet.Bind(_sessionDateInput).For(Constants.INPUT_VALIDATION_TEXT).To(vm => vm.SessionDate).WithConversion(new DateToShortDateNullValueConverter()).OneWay();
            bindingSet.Bind(_sessionGameInput).For(Constants.INPUT_VALIDATION_TEXT).To(vm => vm.SelectedGame.Name);
            //bindingSet.Bind(_sessionPlayerInput).For(Constants.INPUT_VALIDATION_TEXT).To(vm => vm.SelectedPlayer.Name);

            bindingSet.Bind(_sessionVenueInput).For(Constants.INPUT_VALIDATION_ERROR).To(vm => vm.ValidationErrors[nameof(Core.Models.Session.Venue)]);
            bindingSet.Bind(_sessionDateInput).For(Constants.INPUT_VALIDATION_ERROR).To(vm => vm.ValidationErrors[nameof(Core.Models.Session.SessionDate)]);
            bindingSet.Bind(_sessionGameInput).For(Constants.INPUT_VALIDATION_ERROR).To(vm => vm.ValidationErrors[nameof(Core.Models.Session.Game)]);
            //bindingSet.Bind(_sessionPlayerInput).For(Constants.INPUT_VALIDATION_ERROR).To(vm => vm.ValidationErrors[nameof(Core.Models.Session.Player)]);

            bindingSet.Bind(_selectedPlayersLayout).For(Constants.SELECTED_PLAYERS_VIEW).To(vm => vm.SelectedPlayers);

            bindingSet.Bind(_floatingActionButton).For(Constants.INPUT_CLICK).To(vm => vm.SaveCommand);

            bindingSet.Apply();
        }
    }
}