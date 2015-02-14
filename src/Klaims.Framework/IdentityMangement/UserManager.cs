namespace Klaims.Framework.IdentityMangement
{
	using System;
	using Models;

	public class UserManager : IUserManager<User>
	{
		public IQueryableUserRepository<User> Queryable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Create(User user)
		{
			throw new NotImplementedException();
		}

		public void Update(User user)
		{
			throw new NotImplementedException();
		}

		public void Delete(User user)
		{
			throw new NotImplementedException();
		}

		public User GetByUsername(string username)
		{
			throw new NotImplementedException();
		}

		public User GetByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public User GetById(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}