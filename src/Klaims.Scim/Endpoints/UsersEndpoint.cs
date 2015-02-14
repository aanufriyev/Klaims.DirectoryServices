namespace Klaims.Scim.Endpoints
{
	#region

	using System;

	using Klaims.Scim.Resources;
	using Klaims.Scim.Rest;

	using Microsoft.AspNet.Mvc;

	#endregion

	[Route(ScimConstants.Routes.Templates.Users)]
	public class UsersEndpoint : ScimEndpoint
	{
		private readonly IUserResourceManager<ScimUser> resourceManager;

		public UsersEndpoint(IUserResourceManager<ScimUser> resourceManager)
		{
			this.resourceManager = resourceManager;
		}

		[HttpGet]
		public ScimListResponse<ScimUser> GetAll()
		{
			var queryResults = this.resourceManager.Query("id pr");
			return new ScimListResponse<ScimUser>(queryResults, 1);
		}

		[HttpGet("{id}", Name = "GetByIdRoute")]
		public ScimUser GetById(string id)
		{
			var result = this.resourceManager.GetById(id);
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

		[HttpDelete("{id}")]
		public ScimUser DeleteItem(string id)
		{
			var removed = this.resourceManager.Remove(id, -1);
			if (removed == null)
			{
				throw new Exception("User not found");
			}
			return removed; // 201 No Content
		}
	}
}