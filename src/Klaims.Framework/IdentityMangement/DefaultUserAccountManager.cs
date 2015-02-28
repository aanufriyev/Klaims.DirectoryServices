namespace Klaims.Framework.IdentityMangement
{
	using System;

	using Klaims.Framework.IdentityMangement.Models;

	public class DefaultUserAccountManager : IUserAccountManager<User>
	{
		private readonly IUserAccountRepository<User> userAccountRepository;

		public DefaultUserAccountManager(IUserAccountRepository<User> userAccountRepository)
		{
			this.userAccountRepository = userAccountRepository;
		}

		public IQueryableUserAccountRepository<User> Queryable => this.userAccountRepository as IQueryableUserAccountRepository<User>;

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
			return this.userAccountRepository.FindById(id);
		}
	}
}