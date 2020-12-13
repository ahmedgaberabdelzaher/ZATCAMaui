using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace GAZT
{
	
	public class ErrorMessage
    {
		[Preserve(AllMembers = true)]
		public class Message
		{
			public string lang { get; set; }
			public string value { get; set; }
		}
		[Preserve(AllMembers = true)]
		public class Application
		{
			public string component_id { get; set; }
			public string service_namespace { get; set; }
			public string service_id { get; set; }
			public string service_version { get; set; }
		}
		[Preserve(AllMembers = true)]
		public class ErrorResolution
		{
			public string SAP_Transaction { get; set; }
			public string SAP_Note { get; set; }
		}
		[Preserve(AllMembers = true)]
		public class Errordetail
		{
			public string code { get; set; }
			public string message { get; set; }
			public string propertyref { get; set; }
			public string severity { get; set; }
			public string target { get; set; }
		}
		[Preserve(AllMembers = true)]
		public class Innererror
		{
			public Application application { get; set; }
			public string transactionid { get; set; }
			public string timestamp { get; set; }
			public ErrorResolution Error_Resolution { get; set; }
			public List<Errordetail> errordetails { get; set; }
		}
	
		[Preserve(AllMembers = true)]
		public class Error
		{
			public string code { get; set; }
			public Message message { get; set; }
			public Innererror innererror { get; set; }
		}
		[Preserve(AllMembers = true)]
		public class ErrorObj
		{
			public Error error { get; set; }
		}
	}
}
