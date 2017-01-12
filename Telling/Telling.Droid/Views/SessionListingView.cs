using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Util;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Telling.Core.ViewModels.Sessions;
using Android.Support.V4.App;
using Android.Runtime;
using Android.Views;

namespace Telling.Droid.Views
{
    [Register("telling.droid.views.sessionlistingview")]
    public class SessionListingView : BaseFragment<SessionListingViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        //protected override int LayoutResource => Resource.Layout.SessionListingView;

        //protected override void OnCreate(Bundle bundle)
        //{
        //    base.OnCreate(bundle);
        //    var recylerView = FindViewById<MvxRecyclerView>(Resource.Id.recyclerView);

        //    //var layoutManger = new LinearLayoutManager(Activity);
        //    ////recylerView.HasFixedSize = true;
        //    //recylerView.SetLayoutManager(layoutManger);
        //    //var spacing = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 7, Resources.DisplayMetrics);
        //    ////recylerView.AddItemDecoration(new DashboardSpaceItemDecoration(spacing, true, true, Activity));
        //    ////recylerView.ItemTemplateId = Resource.Layout.sanlam_cover_item;

        //    var bindingSet = this.CreateBindingSet<SessionListingView, SessionListingViewModel>();

        //    bindingSet.Bind(recylerView).For(v => v.ItemsSource).To(vm => vm.SessionsCollection);
        //    //bindingSet.Bind(emptyView).For(v => v.Visibility).To(vm => vm.HasPolicies).WithConversion(new MvxInvertedVisibilityValueConverter());
        //    //bindingSet.Bind(recycleView).For(v => v.Visibility).To(vm => vm.HasPolicies);
        //    //bindingSet.Bind(recycleView).For(c => c.ItemClick).To(vm => vm.ItemClickedCommand);

        //    bindingSet.Apply();
        //}
    }
}
