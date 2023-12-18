using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices
{

    public partial class DeclarationTypesModel
    {
        public bool Issuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public ObservableCollection<DeclarationType> Data { get; set; }
        public int Count { get; set; }
        public Guid Correlationid { get; set; }
    }

    public partial class DeclarationType
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
