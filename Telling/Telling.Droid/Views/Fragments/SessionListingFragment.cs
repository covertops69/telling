using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Sessions;
using MvvmCross.Droid.Views.Attributes;
using Refractored.Fab;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    [Register("telling.droid.views.fragments.SessionListingFragment")]
    public class SessionListingFragment : BaseFragment<SessionListingViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_sessionlisting;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);

            var fab = view.FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.AttachToRecyclerView(recyclerView);

            var bindingSet = this.CreateBindingSet<SessionListingFragment, SessionListingViewModel>();
            bindingSet.Bind(fab).For("Click").To(vm => vm.NavigateToAddCommand);
            bindingSet.Apply();

            return view;
        }
    }
}