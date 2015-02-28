namespace Klaims.Framework.IdentityMangement
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	using Klaims.Framework.IdentityMangement.Models;

	public class InMemoryUserAccountRepository : IQueryableUserAccountRepository<User>
	{
		private static readonly List<User> Users = new List<User>
			                                           {
				                                           new User
					                                           {
						                                           Id = Guid.Parse("2819c223-7f76-453a-919d-413861904646"),
						                                           Username = "bjensen@example.com",
						                                           GivenName = "Barbara",
						                                           FamilyName = "Jensen"
					                                           }
			                                           };

		public void Create(User item)
		{
			Users.Add(item);
		}

		public void Remove(User item)
		{
			Users.Remove(item);
		}

		public void Update(User item)
		{
			Users[Users.IndexOf(item)] = item;
		}

		public User FindById(Guid id)
		{
			return Users.FirstOrDefault(user => user.Id == id);
		}

		public User FindByUsername(string username)
		{
			return Users.FirstOrDefault(user => user.Username == username);
		}

		public User FindByEmail(string email)
		{
			return Users.FirstOrDefault(user => user.Emails.Any(e => e.Value == email));
		}

		public IEnumerable<User> Search(Expression<Func<User, bool>> predicate)
		{
			return Users.AsQueryable().Where(predicate).ToList();
		}

		public IEnumerable<User> Search(Expression<Func<User, bool>> predicate, int skip, int count)
		{
			return Users.AsQueryable().Where(predicate).Skip(skip).Take(count).ToList();
		}
	}
}