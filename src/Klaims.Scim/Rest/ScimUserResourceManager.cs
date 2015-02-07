namespace Klaims.Scim.Rest
{
	using System;
	using System.Linq;

	using Klaims.Framework.IdentityMangement;
	using Klaims.Framework.IdentityMangement.Models;
	using Klaims.Framework.Utility;
	using Klaims.Scim.Query;
	using Klaims.Scim.Resources;

	public class ScimUserResourceResourceManager : IUserResourceManager<ScimUser>
	{
		private readonly ISearchQueryConverter<UserAccount> searchQueryConverter;

		private readonly IQueryableUserRepository<UserAccount> userRepository;

		public ScimUserResourceResourceManager(IQueryableUserRepository<UserAccount> userRepository)
		{
			this.userRepository = userRepository;
			searchQueryConverter = new ScimSearchQueryConverter<UserAccount>();
		}

		public SearchResults<ScimUser> Query(string filter)
		{
			var filterPredicate = searchQueryConverter.Convert(filter, null, true);
			var result = userRepository.Users.Where(filterPredicate).ToList();
			return null;
		}

		public SearchResults<ScimUser> Query(string filter, int skip, int count)
		{
			throw new NotImplementedException();
		}

		public ScimUser GetById(string id)
		{
			Check.Argument.IsNotNullOrEmpty(id, "id");
			var userAccount = userRepository.FindById(Guid.Parse(id));

			return null;
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