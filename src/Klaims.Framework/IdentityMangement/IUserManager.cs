namespace Klaims.Framework.IdentityMangement
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Models;

	public interface IUserManager<TUser> where TUser : User
	{
		IQueryableUserRepository<User> Queryable { get; }

		void Create(TUser user);

		void Update(TUser user);

		void Delete(TUser user);

		TUser GetByUsername(string username);

		TUser GetByEmail(string email);

		TUser GetById(Guid id);

	}
}
