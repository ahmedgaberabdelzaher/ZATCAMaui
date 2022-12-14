using System;
using System.Collections.Generic;
using EGAZT.Helper;
namespace EGAZT.Models.EDeclerationsModel.SubmitModels
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ResponseCurrency
    {
        public string typeName_Arabic { get; set; }
        public string typeName_English { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(typeName_Arabic, typeName_English);
            }
        }
        public int value { get; set; }
        public int currency { get; set; }
        public string currencyName_Arabic { get; set; }
        public string currencyName_English { get; set; }
        public string purpose_Arabic { get; set; }
        public string purpose_English { get; set; }
        public string otherpurpose { get; set; }
    }

    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
    }

    public class SubmitResponseProduct
    {
        public string typeName_Arabic { get; set; }
        public string typeName_English { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(typeName_Arabic, typeName_English);
            }
        }
        public string itemCode { get; set; }
        public int count { get; set; }
        public int value { get; set; }
        public string currency_Arabic { get; set; }
        public string currency_English { get; set; }
    }

    public class SubmitResponseRestricted
    {
        public string typeName_Arabic { get; set; }
        public string typeName_English { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(typeName_Arabic, typeName_English);
            }
        }
        public int count { get; set; }
        public int value { get; set; }
        public int currency { get; set; }
        public string currencyName_Arabic { get; set; }
        public string currencyName_English { get; set; }
        public string purpose_Arabic { get; set; }
        public string purpose_English { get; set; }
        public string otherpurpose { get; set; }
        public string unit_Arabic { get; set; }
        public string unit_English { get; set; }
        public bool permit { get; set; }
    }

    public class Result
    {
        public TravelerDeclarationResponse travelerDeclarationResponse { get; set; }
    }

    public class EDeclerationSubmitResponseModel
    {
        public Header header { get; set; }
        public Result result { get; set; }
    }

    public class Status
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class SubmitResponseTobacco
    {
        public string typeName_Arabic { get; set; }
        public string typeName_English { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(typeName_Arabic, typeName_English);
            }
        }
        public string subTypeName_Arabic { get; set; }
        public string subTypeName_English { get; set; }
        public string itemCode { get; set; }
        public int taxSequence { get; set; }
        public int count { get; set; }
        public int measurementUnit { get; set; }
    }

    public class TravelerDeclarationResponse
    {
        public string feedback_Arabic { get; set; }
        public string feedback_Enlgish { get; set; }
        public string FeedbackName
        {
            get
            {
                return Helper.NameLocalization.GetLocalizedName(feedback_Arabic, feedback_Enlgish);
            }
        }
        public string NID { get; set; }
        public DateTime creationDate { get; set; }
        public string ReferenceID { get; set; }
        public bool paymentIsRequired { get; set; }
        public bool paymentIsCompleted { get; set; }
        public bool IsPaid
        {
            get
            {
                return paymentIsRequired && !paymentIsCompleted ? true :false;
            }
        }
        public double totalFees { get; set; }
        public string paymentOrder { get; set; }
        public long sadadNumber { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string travelID { get; set; }
        public int travelDocumentType { get; set; }
        public string travelDocumentType_Arabic { get; set; }
        public string travelDocumentType_English { get; set; }
        public string DocumentTypeName
        {
            get
            {
                return Helper.NameLocalization.GetLocalizedName(travelDocumentType_Arabic, travelDocumentType_English);
            }
        }
        public string arrivingFromDepartingTo_Arabic { get; set; }
        public string arrivingFromDepartingTo_English { get; set; }
        public string ArrivingName
        {
            get
            {
                return Helper.NameLocalization.GetLocalizedName(arrivingFromDepartingTo_Arabic, arrivingFromDepartingTo_English);
            }
        }
        public int portCode { get; set; }
        public string portName_Arabic { get; set; }
        public string portName_English { get; set; }
        public string PortName
        {
            get
            {
                return Helper.NameLocalization.GetLocalizedName(portName_Arabic, portName_English);
            }
        }
        public string flightNumber { get; set; }
        public DateTime travelDate { get; set; }
        public string TravelDateString
        {
            get
            {
                return Helper.DateTimeHelper.DatetimeFormater(travelDate);
            }
        }
        public int travelingType { get; set; }
        public string screenName { get; set; }
        public List<SubmitResponseTobacco> tobacco { get; set; }
        public List<SubmitResponseProduct> product { get; set; }
        public List<ResponseCurrency> currency { get; set; }
        public List<SubmitResponseRestricted> restricted { get; set; }
    }

    public class InquireResponse
    {
        public TravelerDeclarationResponse travelerDeclaration { get; set; }
    }
}

