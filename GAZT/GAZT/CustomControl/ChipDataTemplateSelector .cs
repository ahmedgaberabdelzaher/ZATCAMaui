using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    public class ChipDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HappyEmojiTemplate { get; set; }
        public DataTemplate SadEmojiTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //return (item as ChipModel).CanSelect ? HappyEmojiTemplate : SadEmojiTemplate;
            return  SadEmojiTemplate;
        }
    }
}
