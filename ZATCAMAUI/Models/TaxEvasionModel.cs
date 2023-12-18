using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ZATCAMAUI.Models
{

    public class TaxEvasionModel
    {
        public TaxEvasionModel()
        {
        }
    }

    public partial class TaxEvasionCategoriesModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public TaxEvasionCategoriesDataModel[] Data { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }

    public partial class TaxEvasionCategoriesDataModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("related")]
        public long Related { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public bool IsTypeSelected { get; set; }
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

    public partial class TaxEvasionVerifySmsResponseErrorModel : TaxEvasionVerifySmsResponseModel
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
            return objectType == typeof(TaxEvasionVerifySmsResponseData);
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
        public string _status { get; set; }
        [JsonProperty("status")]
        public string Status
        {
            get
            {
                if (_status == null)
                {
                    return string.Empty;
                }
                else
                {

                    string newStatus = string.Empty;
                    if (_status.Contains("Completed") || _status.Contains("تم التنفيذ"))
                    {
                        newStatus = AppResources.ZTEReportStatusCompleted;
                        _status = newStatus;

                    }

/* Unmerged change from project 'ZATCAMAUI (net7.0-android33.0)'
Before:
                       else if (_status.Contains("New")|| _status.Contains("تم فتح الطلب"))
                        
                        {
                           // newStatus = AppResources.ZTEReportStatusNew;
After:
                       else if (_status.Contains("New")|| _status.Contains("تم فتح الطلب"))

                    {
                        // newStatus = AppResources.ZTEReportStatusNew;
*/
                    else if (_status.Contains("New") || _status.Contains("تم فتح الطلب"))

                    {
                        // newStatus = AppResources.ZTEReportStatusNew;
                        newStatus = AppResources.ZTEReportReportOpen;
                        _status = newStatus;
                    }
                    else
                    {
                        _status = _status;
                    }

                    return _status;

                }
            }
            set
            {
                _status = value;
            }
        }

        [JsonProperty("statusNO")]
        public long StatusNo { get; set; }

        public string _ticketId { get; set; }
        [JsonProperty("ticket_id")]
        public string TicketId
        {
            get
            {
                if (_ticketId == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _ticketId;
                }
            }
            set
            {
                _ticketId = value;
            }
        }

        public string _department { get; set; }
        [JsonProperty("department")]
        public string Department
        {
            get
            {
                if (_department == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _department;
                }
            }
            set
            {
                _department = value;
            }
        }

        public string _subject { get; set; }
        [JsonProperty("subject")]
        public string Subject
        {
            get
            {
                if (_subject == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _subject;
                }
            }
            set
            {
                _subject = value;
            }
        }

        public string _createdAt { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt
        {
            get
            {
                if (_createdAt == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _createdAt;
                }
            }
            set
            {
                string json = string.Empty;
                if (value.Contains("/"))
                {
                    DateTime date = DateTime.ParseExact(value, "dd/MM/yyyy", new CultureInfo("en-US"));
                    json = JsonConvert.SerializeObject(date,
                           new IsoDateTimeConverter() { DateTimeFormat = "dd-MMMM-yyyy" });
                    json = json.Replace("\"", "");

                }
                _createdAt = json;
            }
        }

        public string _facilities { get; set; }
        [JsonProperty("facilities")]
        public string Facilities
        {
            get
            {
                if (_facilities == null)
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

        public string _phoneNumber { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber
        {
            get
            {
                if (_phoneNumber == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _phoneNumber;
                }
            }
            set
            {
                _phoneNumber = value;
            }
        }

        public string EmailId { get; set; }

        public string _location { get; set; }
        [JsonProperty("location")]
        public string Location
        {
            get
            {
                if (_location == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _location;
                }
            }
            set
            {
                _location = value;
            }
        }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string _vatNumber { get; set; }
        [JsonProperty("vat_number")]
        public string VatNumber
        {
            get
            {
                if (_vatNumber == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _vatNumber;
                }
            }
            set
            {
                _vatNumber = value;
            }
        }

        public string _category { get; set; }
        [JsonProperty("category")]
        public string Category
        {
            get
            {
                if (_category == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _category;
                }
            }
            set
            {
                _category = value;
            }
        }

        public string _categoryTitle { get; set; }
        public string CategoryTitle
        {
            get
            {
                if (_categoryTitle == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _categoryTitle;
                }
            }
            set
            {
                _categoryTitle = value;
            }
        }

        public string _content { get; set; }
        [JsonProperty("content")]
        public string Content
        {
            get
            {
                if (_content == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _content;
                }
            }
            set
            {
                _content = value;
            }
        }

        public string _companyName { get; set; }
        [JsonProperty("company_name")]
        public string CompanyName
        {
            get
            {
                if (_companyName == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _companyName;
                }
            }
            set
            {
                _companyName = value;
            }
        }

        public string _workType { get; set; }
        [JsonProperty("work_type")]
        public string WorkType
        {
            get
            {
                if (_workType == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _workType;
                }
            }
            set
            {
                _workType = value;
            }
        }

        public string _city { get; set; }
        [JsonProperty("city")]
        public string City
        {
            get
            {
                if (_city == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _city;
                }
            }
            set
            {
                _city = value;
            }
        }

        public string _username { get; set; }
        [JsonProperty("username")]
        public string Username
        {
            get
            {
                if (_username == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _username;
                }
            }
            set
            {
                _username = value;
            }
        }

        public string _district { get; set; }
        [JsonProperty("district")]
        public string District
        {
            get
            {
                if (_district == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _district;
                }
            }
            set
            {
                _district = value;
            }
        }

        public string _street { get; set; }
        [JsonProperty("street")]
        public string Street
        {
            get
            {
                if (_street == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _street;
                }
            }
            set
            {
                _street = value;
            }
        }

        public string _tin { get; set; }
        [JsonProperty("TIN")]
        public string Tin
        {
            get
            {
                if (_tin == null)
                {
                    return string.Empty;
                }
                else if (_tin == "1234567890")
                {
                    return string.Empty;
                }
                else
                {
                    return _tin;
                }
            }
            set
            {
                _tin = value;
            }
        }

        public string _cr { get; set; }
        [JsonProperty("CR")]
        public string Cr
        {
            get
            {
                if (_cr == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _cr;
                }
            }
            set
            {
                _cr = value;
            }
        }

        public string _id { get; set; }
        [JsonProperty("ID")]
        public string Id
        {
            get
            {
                if (_id == null)
                {
                    return string.Empty;
                }
                else
                {
                    return _id;
                }
            }
            set
            {
                _id = value;
            }
        }

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
