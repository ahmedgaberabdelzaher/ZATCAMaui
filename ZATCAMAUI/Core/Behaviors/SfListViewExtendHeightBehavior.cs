using Syncfusion.Maui.ListView;
using Syncfusion.Maui.ListView.Helpers;
using System.Reflection;

namespace ZATCAMAUI.Core.Behaviors
{
    /// <summary>
    /// This class extends the behavior of the SfListView control to achieve the event to command behavior.
    /// </summary>

    public class SfListViewExtendHeightBehavior : Behavior<SfListView>
    {
        #region Field
        /// <summary>
        /// Gets or sets the visual container of the list view.
        /// </summary>
        private VisualContainer container;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the listView.
        /// </summary>
        public SfListView ListView { get; private set; }
        #endregion
        #region Methods
        /// <summary>
        /// Invoked when adding list view to view.
        /// </summary>
        /// <param name="listView">The SfListView</param>
        protected override void OnAttachedTo(SfListView listView)
        {
            base.OnAttachedTo(listView);
            ListView = listView;
            container = listView.GetVisualContainer();
            container.PropertyChanged += this.Container_PropertyChanged;
        }
        /// <summary>
        /// Invoked when exit from the view.
        /// </summary>
        /// <param name="listView">The SfListView</param>
        protected override void OnDetachingFrom(SfListView listView)
        {
            base.OnDetachingFrom(listView);
            container.PropertyChanged -= this.Container_PropertyChanged;
            container = null;
            ListView = null;
        }
        /// <summary>
        /// Invoked when the container property is changed.
        /// </summary>
        /// <param name="sender">The VisualContainer</param>
        /// <param name="eventArgs">The property changed event args</param>
        private async void Container_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.PropertyName == "Height")
            {
                await Task.Delay(500);
                var extent = (double)container.GetType().GetRuntimeProperties()
                    .FirstOrDefault(container => container.Name == "TotalExtent").GetValue(container);
                ListView.HeightRequest = extent + 1;
            }
        }
        #endregion
    }
}