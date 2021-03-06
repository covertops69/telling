using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Telling.Droid.Views
{
    public abstract class BaseFragment : MvxFragment
    {
        //protected Toolbar Toolbar { get; set; }

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    return base.OnCreateView(inflater, container, savedInstanceState);
        //}
        //{
        //    base.OnCreate(bundle);

        //    SetContentView(LayoutResource);

        //    Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

        //    if (Toolbar != null)
        //    {
        //        SetSupportActionBar(Toolbar);
        //        SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        //        SupportActionBar.SetHomeButtonEnabled(true);
        //    }
        //}

        //protected abstract int LayoutResource { get; }
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment
        where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel => base.ViewModel as TViewModel;
    }
}
