using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.Models.Template
{
    public class ListViewCardTemplateModel
    {
        public string CardLabel { get; set; }
        public string UnSelectedCardIcon { get; set; }
        public string SelectedCardIcon { get; set; }
        public bool IsSelectedCardIconVisible { get => !string.IsNullOrEmpty(SelectedCardIcon) && !string.IsNullOrWhiteSpace(SelectedCardIcon); }
        public bool IsUnSelectedCardIconVisible { get => !string.IsNullOrEmpty(UnSelectedCardIcon) && !string.IsNullOrWhiteSpace(UnSelectedCardIcon); }

        public ListViewCardTemplateModel()
        {
            SelectedCardIcon = "vat_tile_IbanCard_background";
            UnSelectedCardIcon = "vat_tile_IbanCard_background_white";
        }
    }
}
