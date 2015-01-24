namespace Klaims.Scim.Models
{
	#region

	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Klaims.Framework.Utility;

	using Newtonsoft.Json;

	#endregion

	public sealed class UserCoreResource : CoreResource
	{
		private HashSet<Group> groups = new HashSet<Group>();

		private List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();

		public UserCoreResource()
		{
		}

		public UserCoreResource(string id, string userName, string givenName, string familyName)
			: base(id)
		{
			UserName = userName;
			Name = new CommonName(givenName, familyName);
		}

		public string UserName { get; set; }

		public CommonName Name { get; set; }

		public List<Email> Emails { get; set; }

		public HashSet<Group> Groups
		{
			get
			{
				return new HashSet<Group>(groups);
			}
			set
			{
				groups = new HashSet<Group>(value);
			}
		}

		public List<PhoneNumber> PhoneNumbers
		{
			get
			{
				return phoneNumbers;
			}
			set
			{
				if (value != null && value.Any())
				{
					phoneNumbers = value.Where(pn => pn != null && !string.IsNullOrEmpty(pn.Value)).ToList();
				}
				else
				{
					phoneNumbers = value;
				}
			}
		}

		public string DisplayName { get; set; }

		public string NickName { get; set; }

		public string ProfileUrl { get; set; }

		public string Title { get; set; }

		public string UserType { get; set; }

		public string PreferredLanguage { get; set; }

		public string Locale { get; set; }

		public string Timezone { get; set; }

		public bool Active { get; set; } = true;

		public bool Verified { get; set; } = false;

		public string Origin { get; set; } = "";

		public string Password { get; set; }

		[JsonIgnore]
		public string PrimaryEmail
		{
			get
			{
				if (Emails == null || !Emails.Any())
				{
					return null;
				}

				var primaryEmail = Emails.FirstOrDefault(m => m.Primary) ?? Emails[0];
				return primaryEmail.Value;
			}
			set
			{
				var newPrimaryEmail = new Email { Primary = true, Value = value };
				if (Emails == null)
				{
					Emails = new List<Email>(1);
				}

				var currentPrimaryEmail = Emails.FirstOrDefault(m => m.Primary);
				if (currentPrimaryEmail != null)
				{
					Emails.Remove(currentPrimaryEmail);
				}

				Emails.Insert(0, newPrimaryEmail);
			}
		}

		[JsonIgnore]
		public string GivenName => Name?.GivenName;

		[JsonIgnore]
		public string FamilyName => Name?.FamilyName;

		public override string[] Schemas => new[] { "urn:ietf:params:scim:schemas:core:2.0:User" };

		public void AddEmail(string newEmail)
		{
			Check.Argument.IsNotNullOrEmpty(newEmail, "newEmail");

			if (Emails == null)
			{
				Emails = new List<Email>(1);
			}
			if (Emails.Any(m => m.Value.Equals(newEmail)))
			{
				throw new ArgumentException("Already contains email " + newEmail);
			}
			var e = new Email { Value = newEmail };
			Emails.Add(e);
		}

		public void AddPhoneNumber(string newPhoneNumber)
		{
			if (newPhoneNumber == null || newPhoneNumber.Trim().Length == 0)
			{
				return;
			}

			if (phoneNumbers == null)
			{
				phoneNumbers = new List<PhoneNumber>(1);
			}
			if (PhoneNumbers.Any(m => m.Value.Equals(newPhoneNumber)))
			{
				throw new ArgumentException("Already contains phoneNumber " + newPhoneNumber);
			}

			var number = new PhoneNumber { Value = newPhoneNumber };
			phoneNumbers.Add(number);
		}

		public sealed class Group
		{
			public Group()
				: this(null, null)
			{
			}

			public Group(string value, string display)
				: this(value, display, MembershipType.Direct)
			{
			}

			public Group(string value, string display, MembershipType type)
			{
				Value = value;
				Display = display;
				Type = type;
			}

			public string Value { get; set; }

			public string Display { get; set; }

			public MembershipType Type { get; set; }

			public override int GetHashCode()
			{
				const int Prime = 31;
				var result = 1;
				result = Prime * result + (Display?.GetHashCode() ?? 0);
				result = Prime * result + (Value?.GetHashCode() ?? 0);
				result = Prime * result + (Type?.GetHashCode() ?? 0);
				return result;
			}

			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				if (obj == null)
				{
					return false;
				}
				if (GetType() != obj.GetType())
				{
					return false;
				}
				var other = (Group)obj;
				if (Display == null)
				{
					if (other.Display != null)
					{
						return false;
					}
				}
				else if (!Display.Equals(other.Display))
				{
					return false;
				}
				if (Value == null)
				{
					if (other.Value != null)
					{
						return false;
					}
				}
				else if (!Value.Equals(other.Value))
				{
					return false;
				}
				return Type == other.Type;
			}

			public override string ToString()
			{
				return string.Format("(id: {0}, name: {1}, type: {2})", Value, Display, Type);
			}

			public class MembershipType
			{
				public static readonly MembershipType Direct = new MembershipType("direct");

				public static readonly MembershipType Indirect = new MembershipType("indirect");

				private readonly string name;

				private MembershipType(string name)
				{
					this.name = name;
				}

				public override string ToString()
				{
					return name;
				}
			}
		}

		public sealed class CommonName
		{
			public CommonName()
			{
			}

			public CommonName(string givenName, string familyName)
			{
				GivenName = givenName;
				FamilyName = familyName;
				Formatted = givenName + " " + familyName;
			}

			public string FamilyName { get; set; }

			public string Formatted { get; set; }

			public string GivenName { get; set; }

			public string HonorificPrefix { get; set; }

			public string HonorificSuffix { get; set; }

			public string MiddleName { get; set; }
		}

		public sealed class Email
		{
			public bool Primary { get; set; }

			public EmailType Type { get; set; }

			public string Value { get; set; }

			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}

				var email = (Email)obj;

				if (Primary != email.Primary)
				{
					return false;
				}
				if (!Type?.Equals(email.Type) ?? email.Type != null)
				{
					return false;
				}
				if (!Value?.Equals(email.Value) ?? email.Value != null)
				{
					return false;
				}

				return true;
			}

			public override int GetHashCode()
			{
				var result = Value?.GetHashCode() ?? 0;
				result = 31 * result + (Type?.GetHashCode() ?? 0);
				result = 31 * result + (Primary ? 1 : 0);
				return result;
			}
			public class EmailType
			{
				public static readonly EmailType Work = new EmailType("work");

				public static readonly EmailType Home = new EmailType("home");

				private readonly string name;

				private EmailType(string name)
				{
					this.name = name;
				}

				public override bool Equals(object obj)
				{
					var other = obj as EmailType;
					if (other != null && other.name.Equals(this.name))
					{
						return true;
					}
					return false;
				}

				public override int GetHashCode()
				{
					return name.GetHashCode();
				}

				public override string ToString()
				{
					return name;
				}
			}
		}

		public sealed class PhoneNumber
		{
			public string Type { get; set; }

			public string Value { get; set; }
		}
	}
}