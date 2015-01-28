namespace Klaims.Scim.Models
{
	#region

	using System;

	#endregion

	public class ResourceMetadata
	{
		public ResourceMetadata()
		{
		}

		public ResourceMetadata(string resourceType, DateTime created, DateTime lastModified, int version)
		{
			ResourceType = resourceType;
			Created = created;
			LastModified = lastModified;
			Version = version;
		}

		public string ResourceType { get; set; }

		public int Version { get; set; }

		public DateTime? Created { get; set; } = new DateTime();

		public DateTime? LastModified { get; set; }
	}
}