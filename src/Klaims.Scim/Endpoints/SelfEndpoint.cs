namespace Klaims.Scim.Endpoints
{
	using System;

	using Klaims.Scim.Resources;
	using Klaims.Scim.Services;

	using Microsoft.AspNet.Mvc;

	[Route(ScimConstants.Routes.Templates.Self)]
	public class SelfEndpoint : ScimEndpoint
	{
		private readonly IScimUserManager resourceManager;

		public SelfEndpoint(IScimUserManager resourceManager)
		{
			this.resourceManager = resourceManager;
		}

		// Just a stub for now. Assume this claim is required.
		private string UserId => this.User.FindFirst("urn:claims.userId").Value;

		public ScimUser Get()
		{
			var result = this.resourceManager.FindById(this.UserId);

			if (result == null)
			{
				// this will be mappped later in filter
				throw new Exception("User not found");
			}

			return result;
		}

		[HttpPost]
		public void Create([FromBody] ScimUser item)
		{
			if (!this.ModelState.IsValid)
			{
				this.Context.Response.StatusCode = 400;
			}
			else
			{
				this.resourceManager.Create(item);
				var url = this.Url.RouteUrl("GetByIdRoute", new { id = item.Id }, this.Request.Scheme, this.Request.Host.ToUriComponent());
				this.Context.Response.StatusCode = 201;
				this.Context.Response.Headers["Location"] = url;
			}
		}

		[HttpDelete]
		public ScimUser DeleteItem()
		{
			var removed = this.resourceManager.Remove(this.UserId, -1);
			if (removed == null)
			{
				throw new Exception("User not found");
			}
			return removed; // 201 No Content
		}
	}
}