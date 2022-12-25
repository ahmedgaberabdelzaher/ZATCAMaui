using System;
using System.Collections.Generic;

namespace EGAZT.Models.EDeclerationsModel.FeesCalculators
{
   
    public class Product
    {
        public string harmonizedCode { get; set; }
        public int? value { get; set; }
        public Guid ID { get; set; }
    }

    public class FeesCalculatorBody
    {
        public List<Tobacco> tobacco { get; set; } = new List<Tobacco>() {new Tobacco() { count=0, harmonizedCode="0", sequence=0, value=0} };
        public List<Product> product { get; set; } = new List<Product>() { new Product() {  value=0, harmonizedCode="0"  } };
    }

    public class Tobacco
    {
        public string harmonizedCode { get; set; }
        public int? sequence { get; set; }
        public int count { get; set; }
        public int value { get; set; }
        public Guid ID { get; set; }
    }
}

