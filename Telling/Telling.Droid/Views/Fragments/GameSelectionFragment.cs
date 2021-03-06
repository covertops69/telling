﻿using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Views.Attributes;
using Telling.Core.ViewModels;
using Telling.Core.ViewModels.Games;

namespace Telling.Droid.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    [Register("telling.droid.views.fragments.GameSelectionFragment")]

    public class GameSelectionFragment : BaseFragment<GameSelectionViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_gameselection;

        MvxRecyclerView _recyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);

            var bindingSet = this.CreateBindingSet<GameSelectionFragment, GameSelectionViewModel>();
            bindingSet.Bind(_recyclerView).For(c => c.ItemClick).To(vm => vm.ItemSelectedCommand);
            bindingSet.Apply();

            return view;
        }
    }
}