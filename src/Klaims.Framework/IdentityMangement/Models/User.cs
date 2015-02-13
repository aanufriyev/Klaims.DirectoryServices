namespace Klaims.Framework.IdentityMangement.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	using Klaims.Framework.Utility;

	public class User
	{
		public virtual Guid Id { get; protected internal set; }

		[Required]
		public virtual string Username { get; protected internal set; }
		public virtual string DisplayName { get; protected internal set; }
		public virtual string Nickname { get; protected internal set; }
		public virtual string FamilyName { get; protected internal set; }
		public virtual string Formatted { get; protected internal set; }
		public virtual string GivenName { get; protected internal set; }
		public virtual string HonorificPrefix { get; protected internal set; }
		public virtual string HonorificSuffix { get; protected internal set; }
		public virtual string MiddleName { get; protected internal set; }

		public virtual string ProfileUrl { get; protected internal set; }
		public virtual string Title { get; protected internal set; }
		public virtual string PreferredLanguage { get; protected internal set; }
		public virtual string Locale { get; protected internal set; }
		public virtual string Timezone { get; protected internal set; }

		public virtual DateTime Created { get; protected internal set; }
		public virtual DateTime LastUpdated { get; protected internal set; }

		public virtual bool IsAccountClosed { get; protected internal set; }
		public virtual DateTime? AccountClosed { get; protected internal set; }

		public virtual bool IsLoginAllowed { get; protected internal set; }
		public virtual DateTime? LastLogin { get; protected internal set; }
		public virtual DateTime? LastFailedLogin { get; protected internal set; }
		public virtual int FailedLoginCount { get; protected internal set; }

		public virtual DateTime? PasswordChanged { get; protected internal set; }
		public virtual bool RequiresPasswordReset { get; protected internal set; }
		public virtual bool IsAccountVerified { get; protected internal set; }

		public virtual DateTime? LastFailedPasswordReset { get; protected internal set; }
		public virtual int FailedPasswordResetCount { get; protected internal set; }

		// Emails 

		protected virtual ICollection<UserEmail> EmailCollection { get; set; }
		protected internal void AddEmail(UserEmail item) => EmailCollection.Add(item);
		protected internal void RemoveEmail(UserEmail item) => EmailCollection.Remove(item);
		public IEnumerable<UserEmail> Emails => EmailCollection;
		public string PrimaryEmail
		{
			get
			{
				if (EmailCollection == null || !EmailCollection.Any())
				{
					return null;
				}

				var primaryEmail = EmailCollection.FirstOrDefault(m => m.Primary) ?? EmailCollection.First();
				return primaryEmail.Value;
			}
			set
			{
				Ensure.Collection.IsNotNull(EmailCollection);

				var newPrimaryEmail = EmailCollection.FirstOrDefault(m => m.Value == value);
				if (newPrimaryEmail == null)
				{
					// Just exit for now.
					return;
				}

				var currentPrimaryEmail = EmailCollection.FirstOrDefault(m => m.Primary);
				if (currentPrimaryEmail != null)
				{
					currentPrimaryEmail.Primary = false;
				}
				newPrimaryEmail.Primary = true;
			}
		}


		// Claims 
		protected virtual ICollection<UserClaim> ClaimCollection { get; set; }
		protected internal void AddClaim(UserClaim item) => ClaimCollection.Add(item);
		protected internal void RemoveClaim(UserClaim item) => ClaimCollection.Remove(item);
		public IEnumerable<UserClaim> Claims => ClaimCollection;


		// Phones (work and mobile)
		protected virtual ICollection<UserPhone> PhoneCollection { get; set; }
		
		protected internal void AddUserPhone(UserPhone item)=> PhoneCollection.Add(item);
		protected internal void RemoveUserPhone(UserPhone item) => PhoneCollection.Remove(item);
		public IEnumerable<UserPhone> Phones => PhoneCollection;


	}
}