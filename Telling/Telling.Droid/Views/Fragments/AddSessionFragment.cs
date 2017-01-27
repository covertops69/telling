using Android.Runtime;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Sessions;
using MvvmCross.Droid.Shared.Attributes;
using Android.Views;
using Android.OS;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("telling.droid.views.fragments.AddSessionFragment")]

    public class AddSessionFragment : BaseFragment<AddSessionViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_addsession;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            return view;
        }
    }
}