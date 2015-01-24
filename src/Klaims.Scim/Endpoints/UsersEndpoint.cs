namespace Klaims.Scim.Endpoints
{
	#region

	using System.Collections.Generic;
	using System.Linq;

	using Klaims.Scim.Models;

	using Microsoft.AspNet.Mvc;

	#endregion

	[Route(ScimConstants.Routes.Templates.Users)]
	public class UsersEndpoint : Controller
	{
		private static readonly List<UserResource> Resources = new List<UserResource> { new UserResource { Id = "1", Title = "First Item" } };

		[HttpGet]
		public IEnumerable<UserResource> GetAll()
		{
			return Resources;
		}

		[HttpGet("{id:string}", Name = "GetByIdRoute")]
		public IActionResult GetById(string id)
		{
			var item = Resources.FirstOrDefault(x => x.Id == id);
			if (item == null)
			{
				return HttpNotFound();
			}

			return new ObjectResult(item);
		}

		[HttpPost]
		public void Create([FromBody] UserResource item)
		{
			if (!ModelState.IsValid)
			{
				Context.Response.StatusCode = 400;
			}
			else
			{
				Resources.Add(item);

				var url = Url.RouteUrl("GetByIdRoute", new { id = item.Id }, Request.Scheme, Request.Host.ToUriComponent());

				Context.Response.StatusCode = 201;
				Context.Response.Headers["Location"] = url;
			}
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteItem(string id)
		{
			var item = Resources.FirstOrDefault(x => x.Id == id);
			if (item == null)
			{
				return HttpNotFound();
			}
			Resources.Remove(item);
			return new HttpStatusCodeResult(204); // 201 No Content
		}
	}
}