﻿namespace Klaims.Scim.Rest.Formatters
{
	using Microsoft.AspNet.Mvc;
	using Microsoft.Net.Http.Headers;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	public class ScimJsonOutputFormatter : JsonOutputFormatter
	{
		public ScimJsonOutputFormatter() 
		{
			this.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			this.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			this.SupportedMediaTypes.Clear();
			this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ScimConstants.ScimMediaType));
		}
	}
}