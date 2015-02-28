using System;

namespace Klaims.Scim.Endpoints.Filters
{
	using Microsoft.AspNet.Mvc;

	public class ScimExceptionFilter :ExceptionFilterAttribute
    {
		// Filed an issue, because cannot get controller here.
		public override void OnException(ExceptionContext context)
		{
			base.OnException(context);
		}
    }
}