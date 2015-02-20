namespace Klaims.Framework.IdentityMangement
{
	using System;

	using Klaims.Framework.IdentityMangement.Models;

	public class DefaultUserAccountAccountManager : IUserAccountManager<User>
	{
		private readonly IUserRepository<User> userRepository;

		public DefaultUserAccountAccountManager(IUserRepository<User> userRepository)
		{
			this.userRepository = userRepository;
		}

		public IQueryableUserRepository<User> Queryable => this.userRepository as IQueryableUserRepository<User>;

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
			return this.userRepository.FindById(id);
		}
	}
}