namespace Klaims.Scim.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Klaims.Framework.IdentityMangement;
	using Klaims.Framework.IdentityMangement.Models;
	using Klaims.Framework.Utility;
	using Klaims.Scim.Query;
	using Klaims.Scim.Resources;

	public class DefaultScimUserManager : IScimUserManager
	{
		private static readonly List<ScimUser> Resources = new List<ScimUser>
			                                                   {
				                                                   new ScimUser(
					                                                   "2819c223-7f76-453a-919d-413861904646",
					                                                   "bjensen@example.com",
					                                                   "Barbara",
					                                                   "Jensen")
			                                                   };

		private readonly ISearchQueryConverter<User> searchQueryConverter;

		private readonly IUserManager<User> userManager;

		public DefaultScimUserManager(IUserManager<User> userManager)
		{
			this.userManager = userManager;
			this.searchQueryConverter = new ScimSearchQueryConverter<User>();
		}

		public IEnumerable<ScimUser> Query(string filter)
		{
			Check.Argument.IsNotNullOrEmpty(filter, "filter");
			return new List<ScimUser>(Resources);
		}

		public IEnumerable<ScimUser> Query(string filter, int skip, int count)
		{
			Check.Argument.IsNotNullOrEmpty(filter, "filter");
			// Looks like crap. Was bad idea to expose queryable.
			var predicate = this.searchQueryConverter.Convert(filter, null, true);
            var results = this.userManager.Queryable.Search(predicate);
			return new List<ScimUser>(Resources);
		}

		public ScimUser GetById(string id)
		{
			Check.Argument.IsNotNullOrEmpty(id, "id");
			return Resources[0];
		}

		public ScimUser Create(ScimUser resource)
		{
			throw new NotImplementedException();
		}

		public ScimUser Update(string id, ScimUser resource)
		{
			throw new NotImplementedException();
		}

		public ScimUser Remove(string id, int version)
		{
			throw new NotImplementedException();
		}

		public ScimUser CreateUser(ScimUser user, string password)
		{
			throw new NotImplementedException();
		}

		public void ChangePassword(string id, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public ScimUser VerifyUser(string id, int version)
		{
			throw new NotImplementedException();
		}
	}
}