namespace Klaims.Scim.Endpoints
{
	#region

	using System.Collections.Generic;
	using System.Linq;

	using Klaims.Scim.Models;

	using Microsoft.AspNet.Mvc;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	#endregion

	[Route(ScimConstants.Routes.Templates.Users)]
	public class UsersEndpoint : Controller
	{
		
		private static readonly List<UserResource> Resources = new List<UserResource> { new UserResource("2819c223-7f76-453a-919d-413861904646", "bjensen@example.com", "Barbara", "Jensen")};

		[HttpGet]
		public IActionResult GetAll()
		{
			// Just for testing until i figure out how to implement something like IControllerConfiguration. Dint want to use IActionResult here.
			var formatter = new JsonOutputFormatter
				                {
					                SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Include }
				                };
			return new JsonResult(Resources, formatter);
		}

		[HttpGet("{id}", Name = "GetByIdRoute")]
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