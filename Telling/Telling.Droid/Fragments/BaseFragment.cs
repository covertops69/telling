using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;

namespace Telling.Droid.Fragments
{
    public abstract class BaseFragment : MvxFragment
    {
        protected abstract int FragmentId { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(FragmentId, null);

            return view;
        }
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