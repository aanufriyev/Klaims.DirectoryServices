namespace Klaims.Scim.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Klaims.Framework.IdentityMangement;
	using Klaims.Framework.IdentityMangement.Models;
	using Klaims.Framework.Utility;
	using Klaims.Scim.Query;
	using Klaims.Scim.Query.Filter;
	using Klaims.Scim.Resources;

	public class DefaultScimUserManager : IScimUserManager
	{
		private readonly IFilterBinder filterBinder;
		private readonly IUserAccountManager<User> userAccountManager;

		public DefaultScimUserManager(IUserAccountManager<User> userAccountManager, IFilterBinder filterBinder)
		{
			this.userAccountManager = userAccountManager;
			this.filterBinder = filterBinder;
		}

		public IEnumerable<ScimUser> Query(string filter)
		{
			Check.Argument.IsNotNullOrEmpty(filter, "filter");
			var filterNode = UriFilterExpressionParser.Parse(filter);
			var predicate = this.filterBinder.Bind<User>(filterNode, null, true);
			var results = this.userAccountManager.Queryable.Search(predicate);
			return results.Select(this.ToScimUser);
		}

		public IEnumerable<ScimUser> Query(string filter, int skip, int count)
		{
			Check.Argument.IsNotNullOrEmpty(filter, "filter");
			var filterNode = UriFilterExpressionParser.Parse(filter);
			var predicate = this.filterBinder.Bind<User>(filterNode, null, true);
			var results = this.userAccountManager.Queryable.Search(predicate);
			return results.Select(this.ToScimUser);
		}

		public ScimUser FindById(string id)
		{
			Check.Argument.IsNotNullOrEmpty(id, "id");
			var result = this.userAccountManager.GetById(Guid.Parse(id));
			return result == null ? null : this.ToScimUser(result);
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

		private ScimUser ToScimUser(User user)
		{
			return new ScimUser(user.Id.ToString(),user.Username,user.GivenName,user.FamilyName);
		}
	}
}