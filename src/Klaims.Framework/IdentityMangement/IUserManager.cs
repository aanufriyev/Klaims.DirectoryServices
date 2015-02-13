namespace Klaims.Framework.IdentityMangement
{
	using System;
	using Models;

	public interface IUserManager<TUser> where TUser : User
	{
		IUserQuery<TUser> Query { get; }

		void Create(TUser user);

		void Update(TUser user);

		void Delete(TUser user);

		TUser GetByUsername(string username);

		TUser GetByEmail(string email);

		TUser GetById(Guid id);

	}
}
