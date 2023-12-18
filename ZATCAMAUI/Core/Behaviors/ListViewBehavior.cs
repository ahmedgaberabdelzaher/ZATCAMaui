using Syncfusion.Maui.ListView;
using Syncfusion.Maui.ListView.Helpers;
using System.Reflection;

namespace ZATCAMAUI.Core.Behaviors
{


    public class ListViewBehavior : Behavior<SfListView>
    {
        private SfListView listView;
        protected override void OnAttachedTo(SfListView bindable)
        {
            listView = bindable;
            listView.Loaded += OnListViewLoaded;
            base.OnAttachedTo(bindable);
        }

        private void OnListViewLoaded(object sender, ListViewLoadedEventArgs e)
        {
            var container = listView.GetVisualContainer();
            var extent = (double)container.GetType().GetRuntimeProperties().FirstOrDefault(x => x.Name == "TotalExtent").GetValue(container);
            listView.HeightRequest = extent;
        }

        protected override void OnDetachingFrom(SfListView bindable)
        {
            listView.Loaded -= OnListViewLoaded;
            base.OnDetachingFrom(bindable);
        }
    }
}
