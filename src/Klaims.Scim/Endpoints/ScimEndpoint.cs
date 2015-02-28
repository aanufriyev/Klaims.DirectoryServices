namespace Klaims.Scim.Endpoints
{
	using System;
	using System.Collections.Generic;
	using System.Net;

	using Klaims.Scim.Endpoints.Filters;
	using Klaims.Scim.Exceptions;

	using Microsoft.AspNet.Mvc;

	[Produces(ScimConstants.ScimMediaType)]
	[ScimExceptionFilter]
	public class ScimEndpoint : Controller
	{
		private static readonly Dictionary<Type, HttpStatusCode> BaseExceptionsMapping = new Dictionary<Type, HttpStatusCode>();

		static ScimEndpoint()
		{
			BaseExceptionsMapping.Add(typeof(ScimResourceConstraintException), HttpStatusCode.PreconditionFailed);
			BaseExceptionsMapping.Add(typeof(ScimResourceConflictException), HttpStatusCode.Conflict);
			BaseExceptionsMapping.Add(typeof(ScimResourceNotFoundException), HttpStatusCode.NotFound);
			BaseExceptionsMapping.Add(typeof(InvalidScimOperationException), HttpStatusCode.BadRequest);
			BaseExceptionsMapping.Add(typeof(NotImplementedException), HttpStatusCode.NotImplemented);
		}

		public IDictionary<Type, HttpStatusCode> BaseExceptionsMap => BaseExceptionsMapping;
	}
}