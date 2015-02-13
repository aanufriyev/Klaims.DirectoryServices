namespace Klaims.Scim.Resources
{
	public abstract class ScimCore
	{
		public ScimMetadata Meta = new ScimMetadata();

		protected ScimCore(string id)
		{
			this.Id = id;
		}

		protected ScimCore()
		{
		}

		public abstract string[] Schemas { get; }

		public string Id { get; }

		public string ExternalId { get; set; }

		public override int GetHashCode()
		{
			return this.Id?.GetHashCode() ?? base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as ScimCore;
			if (other != null)
			{
				return Id.Equals(other.Id);
			}
			var otherId = obj as string;
			return otherId != null && Id.Equals(otherId);
		}
	}
}