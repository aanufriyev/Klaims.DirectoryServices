namespace Klaims.Framework.IdentityMangement
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public interface IUserQuery<TUser> : IUserRepository<TUser>
		where TUser : class
	{
		IQueryable<TUser> Users { get; }

		IEnumerable<TUser> Query(Func<IQueryable<TUser>, IQueryable<TUser>> filter);

		IEnumerable<TUser> Query(Func<IQueryable<TUser>, IQueryable<TUser>> filter, Func<IQueryable<TUser>, IQueryable<TUser>> sort, int skip, int count, out int totalCount);

	}
}