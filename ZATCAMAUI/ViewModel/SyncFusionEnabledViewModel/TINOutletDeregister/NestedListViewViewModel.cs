using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister
{
    public class NestedListViewViewModel : INotifyPropertyChanged
    {
        #region Properties

        public ObservableCollection<ContactInfo_NestedListView> ContactInfo { get; set; }

        public Command<object> OuterListTapCommand { get; set; }
        #endregion

        #region Constructor
        public NestedListViewViewModel()
        {
            OuterListTapCommand = new Command<object>(OnOuterListTapped);
        }

        private void OnOuterListTapped(object obj)
        {
            var item = obj as ContactInfo_NestedListView;
            item.IsInnerListVisible = !item.IsInnerListVisible;
        }
        #endregion

        #region Private Methods

       

        #endregion

        #region Interface Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }

        #endregion

    }
}

