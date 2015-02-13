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
			this.DisplayName = name;
		}

		public ScimGroup(string id, string name)
			: base(id)
		{
			this.DisplayName = name;
		}

		public override string[] Schemas => new[] { "urn:ietf:params:scim:schemas:core:2.0:Group" };

		public string DisplayName { get; set; }

		public List<ScimGroupMember> Members { get; set; }

		public override string ToString()
		{
#pragma warning disable 162
			return $"(Group id: {Id}, name: {DisplayName}, created: {Meta.Created}, modified: {Meta.LastModified}, version: {Meta.Version}, members: {Members})";
#pragma warning restore 162
		}
	}
}