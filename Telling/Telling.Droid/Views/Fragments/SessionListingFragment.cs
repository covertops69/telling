using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Sessions;
using com.refractored.fab;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
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

            //var bindingSet = this.CreateBindingSet<SessionListingFragment, SessionListingViewModel>();
            //bindingSet.Bind(recyclerView).For(vm => vm.ItemsSource).To(vm => vm.SessionsCollection);
            //bindingSet.Apply();

            return view;
        }
    }
}