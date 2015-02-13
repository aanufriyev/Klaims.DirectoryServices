namespace Klaims.Framework.IdentityMangement
{
	using System;

	public interface IUserRepository<TUser> where TUser :class
	{
		TUser Create();
		void Add(TUser item);
		void Remove(TUser item);
		void Update(TUser item);

		TUser FindById(Guid id);
		TUser FindByUsername(string username);
		TUser FindByEmail(string email);
	}
}