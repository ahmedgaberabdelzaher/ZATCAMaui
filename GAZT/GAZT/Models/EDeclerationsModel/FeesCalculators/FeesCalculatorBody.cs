using System;
using System.Collections.Generic;

namespace EGAZT.Models.EDeclerationsModel.FeesCalculators
{
   
    public class Product
    {
        public string harmonizedCode { get; set; }
        public double? value { get; set; }
        public int Count { get; set; }
        public Guid ID { get; set; }
    }

    public class FeesCalculatorBody
    {
        //public List<Tobacco> tobacco { get; set; } = new List<Tobacco>() {new Tobacco() { count=0, harmonizedCode="0", sequence=0, value=0} };
        //public List<Product> product { get; set; } = new List<Product>() { new Product() {  value=0, harmonizedCode="0"  } };

        public List<Tobacco> tobacco { get; set; } 
        public List<Product> product { get; set; }

    }

    public class Tobacco
    {
        public string harmonizedCode { get; set; }
        public int? sequence { get; set; }
        public int count { get; set; }
        public int value { get; set; }
        public int MeasuringUnit { get; set; }
        public int Wight { get; set; }
        public Guid ID { get; set; }
    }



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ProductItemCustomApi
    {
        public string HarmonizedCode { get; set; }
        public double? Value { get; set; }
        public int Count { get; set; }
        public Guid ID { get; set; }
    }

    public class FeesCalculatorBodyCustomApi
    {
        public List<TobaccoItemCustomApi> TobaccoItems { get; set; }
        public List<ProductItemCustomApi> ProductItems { get; set; }
    }

    public class TobaccoItemCustomApi
    {
        public string HarmonizedCode { get; set; }
        public int MeasuringUnit { get; set; }
        public int? Sequence { get; set; }
        public int Count { get; set; }
        public int Value { get; set; }
        public int Wight { get; set; }
        public Guid ID { get; set; }
    }


}

