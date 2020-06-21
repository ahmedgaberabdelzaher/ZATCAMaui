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

        [JsonProperty("facilities")]
        public string Facilities { get; set; }

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


    public enum RegionNameAr { Asir, Bahah, Riyadh };

    public enum Status { Completed };

    public enum Username { Ahmed, AhmedAtef, UsernameAhmedAtef };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                RegionNameArConverter.Singleton,
                StatusConverter.Singleton,
                UsernameConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class RegionNameArConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(RegionNameAr) || t == typeof(RegionNameAr?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Asir":
                    return RegionNameAr.Asir;
                case "Bahah":
                    return RegionNameAr.Bahah;
                case "Riyadh":
                    return RegionNameAr.Riyadh;
            }
            throw new Exception("Cannot unmarshal type RegionNameAr");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (RegionNameAr)untypedValue;
            switch (value)
            {
                case RegionNameAr.Asir:
                    serializer.Serialize(writer, "Asir");
                    return;
                case RegionNameAr.Bahah:
                    serializer.Serialize(writer, "Bahah");
                    return;
                case RegionNameAr.Riyadh:
                    serializer.Serialize(writer, "Riyadh");
                    return;
            }
            throw new Exception("Cannot marshal type RegionNameAr");
        }

        public static readonly RegionNameArConverter Singleton = new RegionNameArConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Completed")
            {
                return Status.Completed;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            if (value == Status.Completed)
            {
                serializer.Serialize(writer, "Completed");
                return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class UsernameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Username) || t == typeof(Username?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Ahmed":
                    return Username.Ahmed;
                case "Ahmed  Atef":
                    return Username.UsernameAhmedAtef;
                case "Ahmed atef":
                    return Username.AhmedAtef;
            }
            throw new Exception("Cannot unmarshal type Username");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Username)untypedValue;
            switch (value)
            {
                case Username.Ahmed:
                    serializer.Serialize(writer, "Ahmed");
                    return;
                case Username.UsernameAhmedAtef:
                    serializer.Serialize(writer, "Ahmed  Atef");
                    return;
                case Username.AhmedAtef:
                    serializer.Serialize(writer, "Ahmed atef");
                    return;
            }
            throw new Exception("Cannot marshal type Username");
        }

        public static readonly UsernameConverter Singleton = new UsernameConverter();
    }
}
