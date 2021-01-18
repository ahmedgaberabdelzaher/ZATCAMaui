using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public  class CustomPicker : Picker
    {
        #region Constructor
        public CustomPicker()
        {
            switch (Device.RuntimePlatform)
            {
                case "iOS":
                    HeightRequest = 40;
                    break;
                case "Android":
                    break;
            }
            SelectedIndexChanged += OnSelectionChanged;
        }
        #endregion Constructor
        #region Fields
        //Bindable property for the items source
        public static readonly BindableProperty DisplayMemberBindingProperty =
            BindableProperty.Create<CustomPicker, string>(p => p.DisplayMemberBinding, string.Empty, BindingMode.TwoWay);
        //Bindable property for the selected item
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create<CustomPicker, object>(p => p.SelectedItem, null, BindingMode.TwoWay, propertyChanged: OnSelectedItemPropertyChanged);
        //Bindable property for the items source
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<CustomPicker, IList>(p => p.ItemsSource, null, propertyChanged: OnItemsSourcePropertyChanged);
        public string DoneButtonText
        {
            get
            {
                return (string)GetValue(DoneButtonTextProperty);
            }
            set
            {
                SetValue(DoneButtonTextProperty, value);
            }
        }
        public static readonly BindableProperty DoneButtonTextProperty = BindableProperty.Create(
                                                          propertyName: "DoneButtonTextProperty",
                                                          returnType: typeof(string),
                                                          declaringType: typeof(CustomPicker),
                                                          defaultValue: string.Empty,
                                                          defaultBindingMode: BindingMode.TwoWay,
                                                          propertyChanged: DoneButtonTextPropertyChanged);
        private static void DoneButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomPicker).DoneButtonText = newValue.ToString();
        }
        public string CancelButtonText
        {
            get
            {
                return (string)GetValue(CancelButtonTextProperty);
            }
            set
            {
                SetValue(CancelButtonTextProperty, value);
            }
        }
        public static readonly BindableProperty CancelButtonTextProperty = BindableProperty.Create(
                                                          propertyName: "CancelButtonTextProperty",
                                                          returnType: typeof(string),
                                                          declaringType: typeof(CustomPicker),
                                                          defaultValue: string.Empty,
                                                          defaultBindingMode: BindingMode.TwoWay,
                                                          propertyChanged: CancelButtonTextPropertyChanged);
        private static void CancelButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomPicker).CancelButtonText = newValue.ToString();
        }
        #endregion Fields
        #region Properties
        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>
        /// The items source.
        /// </value>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public string DisplayMemberBinding
        {
            get { return GetValue(DisplayMemberBindingProperty).ToString(); }
            set { SetValue(DisplayMemberBindingProperty, value); }
        }
        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        //public string DoneButtonText
        //{
        //    get
        //    {
        //        return (string)GetValue(DoneButtonTextProperty);
        //    }
        //    set
        //    {
        //        SetValue(DoneButtonTextProperty, value);
        //    }
        //}
        //public static readonly BindableProperty DoneButtonTextProperty = BindableProperty.Create(
        //propertyName: "DoneButtonTextProperty",
        //returnType: typeof(string),
        //declaringType: typeof(CustomPicker),
        //defaultValue: string.Empty,
        //defaultBindingMode: BindingMode.TwoWay,
        //propertyChanged: DoneButtonTextPropertyChanged);
        //public string CancelButtonText
        //{
        //    get
        //    {
        //        return (string)GetValue(CancelButtonTextProperty);
        //    }
        //    set
        //    {
        //        SetValue(CancelButtonTextProperty, value);
        //    }
        //}
        //public static readonly BindableProperty CancelButtonTextProperty = BindableProperty.Create(
        //propertyName: "CancelButtonTextProperty",
        //returnType: typeof(string),
        //declaringType: typeof(CustomPicker),
        //defaultValue: string.Empty,
        //defaultBindingMode: BindingMode.TwoWay,
        //propertyChanged: CancelButtonTextPropertyChanged);
        #endregion Properties
        #region Methods
        //private static void CancelButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    (bindable as CustomPicker).CancelButtonText = newValue.ToString();
        //}
        //private static void DoneButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    (bindable as CustomPicker).DoneButtonText = newValue.ToString();
        //}
        /// <summary>
        /// Called when [items source property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="value">The value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnItemsSourcePropertyChanged(BindableObject bindable, IEnumerable value, IEnumerable newValue)
        {
            var picker = (CustomPicker)bindable;
            var eventList = picker.GetType().GetRuntimeEvents();
            var notifyCollection = newValue as INotifyCollectionChanged;
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems != null)
                    {
                        foreach (var newItem in args.NewItems)
                        {
                            picker.Items.Add((newItem ?? "").ToString());
                        }
                    }
                    if (args.OldItems != null)
                    {
                        foreach (var oldItem in args.OldItems)
                        {
                            picker.Items.Remove((oldItem ?? "").ToString());
                        }
                    }
                };
            }
            if (newValue == null)
                return;
            picker.Items.Clear();
            if (string.IsNullOrWhiteSpace(picker.DisplayMemberBinding))
                foreach (var item in newValue)
                    picker.Items.Add(item as string);
            else
                foreach (var item in newValue)
                {
                    try
                    {
                        List<FieldInfo> fields = item.GetType().GetRuntimeFields().ToList();
                        var actualValue = string.Empty;
                        foreach (FieldInfo field in fields.ToList())
                        {
                            if (field.Name.Contains(picker.DisplayMemberBinding))
                            {
                                actualValue = field.GetValue(item).ToString();
                            }
                        }
                        picker.Items.Add((actualValue ?? "").ToString());
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }
        }
        /// <summary>
        /// Called when [selected item property changed].
        /// </summary>
        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object value, object newValue)
        {
            var picker = (CustomPicker)bindable;
            if (picker.ItemsSource == null) return;
            var index = picker.ItemsSource.IndexOf(picker.SelectedItem);
            if (picker.SelectedIndex == index)
                return;
            picker.SelectedIndex = index;
        }
        private void OnSelectionChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
                {
                    SelectedItem = null;
                }
                else
                {
                    int count = 0;
                    foreach (var item in ItemsSource)
                    {
                        if (count == SelectedIndex)
                        {
                            SelectedItem = item;
                            break;
                        }
                        count++;
                    }
                }
            }
            catch (Exception )
            {
            }
        }

        #endregion Methods
    }
}
