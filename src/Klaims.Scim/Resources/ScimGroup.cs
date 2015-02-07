namespace Klaims.Scim.Resources
{
	using System.Collections.Generic;

	public class ScimGroup : ScimCore
	{
		public ScimGroup()
		{
		}

		public ScimGroup(string name)
		{
			DisplayName = name;
		}

		public ScimGroup(string id, string name)
			: base(id)
		{
			DisplayName = name;
		}

		public override string[] Schemas => new[] { "urn:ietf:params:scim:schemas:core:2.0:Group" };

		public string DisplayName { get; set; }

		public List<ScimGroupMember> Members { get; set; }

		public override string ToString()
		{
			return string.Format(
				"(Group id: {0}, name: {1}, created: {2}, modified: {3}, version: {4}, members: {5})",
				Id,
				DisplayName,
				Meta.Created,
				Meta.LastModified,
				Meta.Version,
				Members);
		}
	}
}