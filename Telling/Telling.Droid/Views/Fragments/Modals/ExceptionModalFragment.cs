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

using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Modals;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Views.Attributes;

namespace Telling.Droid.Views.Fragments.Modals
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("telling.droid.views.fragments.modals.ExceptionModalFragment")]
    class ExceptionModalFragment : BaseFragment<ModalViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_modal;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var bindingSet = this.CreateBindingSet<ExceptionModalFragment, ModalViewModel>();
            bindingSet.Apply();

            return view;
        }
    }
}