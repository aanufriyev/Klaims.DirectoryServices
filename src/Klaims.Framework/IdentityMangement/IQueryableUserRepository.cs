namespace Klaims.Framework.IdentityMangement
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IQueryableUserRepository<TUser> : IUserRepository<TUser>
		where TUser : class
	{
		IQueryable<TUser> Users { get; }
	}
}