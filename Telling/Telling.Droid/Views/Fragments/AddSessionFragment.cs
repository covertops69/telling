using System;
using Android.Runtime;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Sessions;
using MvvmCross.Droid.Shared.Attributes;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragment(typeof(BaseViewModel), Resource.Id.content_frame)]
    [Register("telling.droid.views.fragments.AddSessionFragment")]

    public class AddSessionFragment : BaseFragment<AddSessionViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_sessionlisting;
    }
}