namespace Klaims.IdentityManagement.EntityFramework
{
	using Klaims.Framework.IdentityMangement.Models;

	using Microsoft.Data.Entity;

	public class IdentityDbContext : DbContext
	{
		public DbSet<UserAccount> UserAccounts { get; set; }

		public DbSet<UserEmail> UserEmails { get; set; }

		public DbSet<UserPhone> UserPhones { get; set; }

		public DbSet<UserClaim> UserClaims { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<UserAccount>(
				b =>
					{
						b.Key(u => u.Id);
						b.ForRelational().Table("UserAccounts");
						b.Property(u => u.Version).ConcurrencyToken();
					});

			builder.Entity<UserClaim>(
				b =>
					{
						b.Key(uc => uc.Id);
						b.HasOne<UserAccount>().WithMany().ForeignKey(uc => uc.UserId);
						b.ForRelational().Table("UserClaims");
					});

			builder.Entity<UserEmail>(
				b =>
					{
						b.Key(rc => rc.Id);
						b.HasOne<UserAccount>().WithMany().ForeignKey(rc => rc.UserId);
						b.ForRelational().Table("UserEmails");
					});

			builder.Entity<UserPhone>(
				b =>
					{
						b.Key(rc => rc.Id);
						b.HasOne<UserPhone>().WithMany().ForeignKey(rc => rc.UserId);
						b.ForRelational().Table("UserPhones");
					});
		}
	}
}