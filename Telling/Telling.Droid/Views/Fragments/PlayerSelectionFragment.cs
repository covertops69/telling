using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views.Attributes;
using MvvmCross.Plugins.Visibility;
using System.Windows.Input;
using Telling.Core;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Players;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    [Register("telling.droid.views.fragments.PlayerSelectionFragment")]

    public class PlayerSelectionFragment : BaseFragment<PlayerSelectionViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_playerselection;

        MvxFluentBindingDescriptionSet<PlayerSelectionFragment, PlayerSelectionViewModel> _bindingSet;
        MvxRecyclerView _recyclerView;

        IMenu _menu;
        IMenuItem _saveMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);

            _bindingSet = this.CreateBindingSet<PlayerSelectionFragment, PlayerSelectionViewModel>();

            _bindingSet.Bind(_recyclerView).For(c => c.ItemClick).To(vm => vm.ItemSelectedCommand);
            _bindingSet.Bind(_recyclerView).For(c => c.ItemsSource).To(vm => vm.PlayersCollection);

            _bindingSet.Apply();

            //ViewModel.PlayerSelectionChanged += PlayerSelectionChanged;

            HasOptionsMenu = true;

            return view;
        }

        private void PlayerSelectionChanged(object sender, PlayerSelectionChangedEventArgs e)
        {
            _saveMenuItem.SetVisible(e.HasPlayers);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            _menu = menu;

            ((AppCompatActivity)Activity).MenuInflater.Inflate(Resource.Menu.menu_playerselection, menu);

            _saveMenuItem = menu.FindItem(Resource.Id.action_save);

            _bindingSet.Bind(_saveMenuItem).For(Constants.MENU_ITEM_VISIBILITY).To(vm => vm.IsPlayerSelected);
            _bindingSet.Apply();

            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_save:
                    ViewModel.SelectedPlayers();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}