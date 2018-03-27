using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views.Attributes;
using System.Windows.Input;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Players;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    [Register("telling.droid.views.fragments.PlayerSelectionFragment")]

    public class PlayerSelectionFragment : BaseFragment<PlayerSelectionViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_playerselection;

        MvxRecyclerView _recyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);

            var bindingSet = this.CreateBindingSet<PlayerSelectionFragment, PlayerSelectionViewModel>();

            bindingSet.Bind(_recyclerView).For(c => c.ItemClick).To(vm => vm.ItemSelectedCommand);
            bindingSet.Bind(_recyclerView).For(c => c.ItemsSource).To(vm => vm.PlayersCollection);
            bindingSet.Apply();

            HasOptionsMenu = true;

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            ((AppCompatActivity)Activity).MenuInflater.Inflate(Resource.Menu.menu1, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }
    }
}