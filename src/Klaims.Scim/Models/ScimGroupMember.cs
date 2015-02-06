namespace Klaims.Scim.Models
{
	using System;
	using System.Collections.Generic;

	using DefaultNamespace;

	using Newtonsoft.Json;

	public class ScimGroupMember
	{
		public enum MemberType
		{
			USER,

			GROUP
		}

		public enum Role
		{
			Member,

			Reader,

			Writer
		}

		public static readonly List<Role> GROUP_MEMBER = new List<Role> { Role.Member };

		public static readonly List<Role> GROUP_ADMIN = new List<Role> { Role.Reader, Role.Writer };

		private string origin = Constants.Origin.Klaims;

		public ScimGroupMember()
		{
		}

		public ScimGroupMember(string memberId)
			: this(memberId, MemberType.USER, GROUP_MEMBER)
		{
		}

		public ScimGroupMember(string memberId, MemberType type, List<Role> roles)
		{
			MemberId = memberId;
			Type = type;
			Roles = roles;
		}

		[JsonProperty("value")]
		public string MemberId { get; }

		public MemberType Type { get; }

		[JsonIgnore]
		public List<Role> Roles { get; set; }

		public string Origin
		{
			get
			{
				return origin;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException();
				}
				origin = value;
			}
		}

		public override string ToString()
		{
			return string.Format("(memberId: {0}, type: {1}, roles: {2})", MemberId, Type, Roles);
		}

		public override bool Equals(Object o)
		{
			if (this == o)
			{
				return true;
			}
			if (o == null || GetType() != o.GetType())
			{
				return false;
			}

			var that = (ScimGroupMember)o;

			if (!MemberId.Equals(that.MemberId))
			{
				return false;
			}
			if (!origin.Equals(that.Origin))
			{
				return false;
			}
			if (Type != that.Type)
			{
				return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			var result = MemberId.GetHashCode();
			result = 31 * result + Origin.GetHashCode();
			result = 31 * result + Type.GetHashCode();
			return result;
		}
	}
}