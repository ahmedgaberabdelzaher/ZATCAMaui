using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace EGAZT.Models
{
    public class TaxEvasionModel
    {
        public TaxEvasionModel()
        {
        }
    }

    public class TaxEvasionSendSmsModel
    {
        [JsonProperty("mobile")]
        public string mobile { get; set; }
    }

    public partial class TaxEvasionSendSmsResponseModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public TaxEvasionSendSmsResponseData Data { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }

    public partial class TaxEvasionSendSmsResponseData
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class TaxEvasionVerifySmsModel
    {
        [JsonProperty("key")]
        public string key { get; set; }

        [JsonProperty("code")]
        public string code { get; set; }
    }

    public partial class TaxEvasionVerifySmsResponseModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public TaxEvasionVerifySmsResponseData SmsResponse { get; set; }

        [JsonProperty("code")]
        public long ResponseStatusCode { get; set; }
    }

    public partial class TaxEvasionVerifySmsResponseErrorModel: TaxEvasionVerifySmsResponseModel
    {
        [JsonProperty("data")]
        public string ErrorResponseMessage { get; set; }
    }

    public partial class TaxEvasionVerifySmsResponseData
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class SmsErrorDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(TaxEvasionVerifySmsResponseData));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Object)
            {
                return token.ToObject<TaxEvasionVerifySmsResponseData>();
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    public partial class TaxEvasionReportsModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public TaxEvasionReportsData Data { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }

    public partial class TaxEvasionReportsData
    {
        [JsonProperty("opened")]
        public TaxEvasionReportDetails[] Opened { get; set; }

        [JsonProperty("closed")]
        public TaxEvasionReportDetails[] Closed { get; set; }
    }


    public partial class TaxEvasionReportDetails
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusNO")]
        public long StatusNo { get; set; }

        [JsonProperty("ticket_id")]
        public string TicketId { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        public string _facilities { get; set; }
        [JsonProperty("facilities")]
        public string Facilities
        {
            get
            {
                if(_facilities == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _facilities;
                }
            }
            set
            {
                _facilities = value;
            }
        }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        public string EmailId { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [JsonProperty("vat_number")]
        public string VatNumber { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("work_type")]
        public string WorkType { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("TIN")]
        public string Tin { get; set; }

        [JsonProperty("CR")]
        public string Cr { get; set; }

        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("RegionCode")]
        public string RegionCode { get; set; }

        [JsonProperty("RegionNameAr")]
        public string RegionNameAr { get; set; }

        [JsonProperty("reply")]
        public string Reply { get; set; }
    }

    public partial class TaxEvasionRegionsCityModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public List<TaxEvasionRegionCityDatum> Data { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }

    public partial class TaxEvasionRegionCityDatum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class TaxEvasionCreateReportResponseModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public TaxEvasionCreateReportResponseData Data { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }

    public partial class TaxEvasionCreateReportResponseData
    {
        [JsonProperty("ticket_id")]
        public string TicketId { get; set; }

        [JsonProperty("ticket_post_id")]
        public Guid TicketPostId { get; set; }
    }

    public partial class TaxEvasionErrorReponseModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }

    public partial class TaxEvasionRegisterUserModel
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }

    public partial class TaxEvasionUserRegistrationResponseModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public TaxEvasionUserRegistrationResponseData Data { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }

    public partial class TaxEvasionUserRegistrationResponseData
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        public string LoginKey { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("password")]
        public object Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("access_token")]
        public object AccessToken { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("confirmation_code")]
        public object ConfirmationCode { get; set; }

        [JsonProperty("confirmed")]
        public long Confirmed { get; set; }

        [JsonProperty("remember_token")]
        public object RememberToken { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("deleted_at")]
        public string DeletedAt { get; set; }

        [JsonProperty("ios_version")]
        public string IosVersion { get; set; }

        [JsonProperty("android_version")]
        public string AndroidVersion { get; set; }

        [JsonProperty("api_token")]
        public string ApiToken { get; set; }
    }
}
