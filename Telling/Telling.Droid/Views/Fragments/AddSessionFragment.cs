using Android.Runtime;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Sessions;
using MvvmCross.Droid.Shared.Attributes;
using Android.Views;
using Android.OS;
using Telling.Droid.Controls;
using System;
using MvvmCross.Binding.BindingContext;
using Telling.Core.Converters;
using Android.Views.InputMethods;
using com.refractored.fab;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("telling.droid.views.fragments.AddSessionFragment")]

    public class AddSessionFragment : BaseFragment<AddSessionViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_addsession;

        TInputValidation _sessionVenueInput, _sessionDateInput;
        FloatingActionButton _floatingActionButton;

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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _sessionVenueInput = view.FindViewById<TInputValidation>(Resource.Id.session_venue);
            _sessionDateInput = view.FindViewById<TInputValidation>(Resource.Id.session_date);

            _floatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.fab);

            _sessionDateInput.EditTextControl.SetCursorVisible(false);
            _sessionDateInput.EditTextControl.ShowSoftInputOnFocus = false;

            Bind();

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();

            _sessionDateInput.EditTextControl.Click += DateInputTap();
            _sessionDateInput.EditTextControl.FocusChange += DateInputFocusChange();
        }

        public override void OnPause()
        {
            base.OnPause();

            _sessionDateInput.EditTextControl.Click -= DateInputTap();
            _sessionDateInput.EditTextControl.FocusChange -= DateInputFocusChange();
        }

        void Bind()
        {
            var bindingSet = this.CreateBindingSet<AddSessionFragment, AddSessionViewModel>();

            bindingSet.Bind(_sessionVenueInput).To(vm => vm.Venue);
            bindingSet.Bind(_sessionDateInput).For(cntrl => cntrl.Text).To(vm => vm.SessionDate).WithConversion(new DateToShortDateNullValueConverter()).OneWay();

            bindingSet.Bind(_floatingActionButton).For("Click").To(vm => vm.SaveCommand);

            bindingSet.Apply();
        }
    }
}