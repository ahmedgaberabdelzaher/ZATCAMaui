using System;
namespace EGAZT.Models.EDeclerationsModel
{
    public class EDeclerationCardModel
    {
        public string Name { get; set; }
        public string desc { get; set; }
        public string Price { get; set; }
        public string QTY { get; set; }
        public Guid ID { get; set; }
        public int Type { get; set; }
        public bool HasLine { get; set; } = true;
    }
}

