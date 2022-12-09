using System;
using System.Collections.Generic;

namespace EGAZT.Models.EDeclerationsModel.SubmitModels
{

    public class Currency
    {
        public string typeName { get; set; }
        public int typeID { get; set; }
        public int purpose { get; set; }
        public string otherpurpose { get; set; }
        public int value { get; set; }
        public int currency { get; set; }
        public string currencyName { get; set; }
    }

    public class Product
    {
        public string typeName { get; set; }
        public long? itemCode { get; set; }
        public int count { get; set; }
        public int value { get; set; }
    }

    public class Restricted
    {
        public string typeName { get; set; }
        public int purpose { get; set; }
        public string otherpurpose { get; set; }
        public int count { get; set; }
        public int unit { get; set; }
        public int value { get; set; }
        public int currency { get; set; }
        public string currencyName { get; set; }
        public bool permit { get; set; }
        public string attachment { get; set; }
    }

    public class EDeclerationSubmitModel
    {
        public TravelerDeclaration travelerDeclaration { get; set; } = new TravelerDeclaration();
    }

    public class Tobacco
    {
        public string typeName { get; set; }
        public string subTypeName { get; set; }
        public long? itemCode { get; set; }
        public int? taxSequence { get; set; }
        public int? count { get; set; }
        public int? measurementUnit { get; set; }
        public int? value { get; set; }
    }

    public class TravelerDeclaration
    {
        public int travelingType { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string travelID { get; set; }
        public int travelIssuerID { get; set; }
        public int nationality { get; set; }
        public int gender { get; set; }
        public int port { get; set; }
        public int arrivingFromDepartingTo { get; set; }
        public DateTime travelDate { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public int travelersCount { get; set; }
        public DateTime birthDate { get; set; }
        public int travelPurpose { get; set; }
        public string address { get; set; }
        public string flightNumber { get; set; }
        public int tripeType { get; set; }
        public int travelDocumentType { get; set; }
        public int passIssuingCountry { get; set; }
        public DateTime passIssuingDate { get; set; }
        public DateTime passExpiryDate { get; set; }
        public List<Tobacco> tobacco { get; set; } = new List<Tobacco>();
        public List<Product> product { get; set; } = new List<Product>();
        public List<Currency> currency { get; set; }
        public List<Restricted> restricted { get; set; }
    }
}

