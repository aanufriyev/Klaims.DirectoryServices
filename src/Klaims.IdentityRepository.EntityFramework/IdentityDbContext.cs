namespace Klaims.IdentityManagement.EntityFramework
{
	using Klaims.Framework.IdentityMangement.Models;

	using Microsoft.Data.Entity;

	public class IdentityDbContext : DbContext
	{
		public DbSet<UserAccount> UserAccounts { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<UserAccount>(
				b =>
					{
						b.Key(u => u.Id);
						b.ForRelational().Table("UserAccounts");
						b.Property(u => u.Version).ConcurrencyToken();
					});

		}
	}
}