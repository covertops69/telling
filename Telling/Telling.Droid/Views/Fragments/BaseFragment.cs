using Android.OS;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Graphics;
using Android.Support.V7.App;
using MvvmCross.Binding.BindingContext;
using Telling.Core.ViewModels;

namespace Telling.Droid.Views.Fragments
{
    public abstract class BaseFragment : MvxFragment
    {
        protected abstract int FragmentId { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(FragmentId, null);

            view.SetBackgroundColor(Color.Transparent);

            var supportBar = ((AppCompatActivity)Activity).SupportActionBar;

            var bindingSet = this.CreateBindingSet<BaseFragment, BaseViewModel>();
            bindingSet.Bind(supportBar).For(c => c.Title).To(vm => vm.Title);
            bindingSet.Apply();

            if (!(this is SessionListingFragment))
            {
                supportBar.SetDisplayHomeAsUpEnabled(true);
                supportBar.SetDisplayShowHomeEnabled(true);
            }
            else
            {
                supportBar.SetDisplayHomeAsUpEnabled(false);
                supportBar.SetDisplayShowHomeEnabled(false);
            }

            return view;
        }
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment
        where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel => base.ViewModel as TViewModel;
    }
}