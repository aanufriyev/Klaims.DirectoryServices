namespace Klaims.Framework.IdentityMangement
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Models;

	public interface IUserAccountManager<TAccount> where TAccount : User
	{
		IQueryableUserRepository<User> Queryable { get; }

		void Create(TAccount user);

		void Update(TAccount user);

		void Delete(TAccount user);

		TAccount GetByUsername(string username);

		TAccount GetByEmail(string email);

		TAccount GetById(Guid id);

	}
}
