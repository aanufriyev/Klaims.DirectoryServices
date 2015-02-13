namespace Klaims.Scim.Query
{
	using System.Collections.Generic;
	using System.Text;

	public class SearchResults<TResource>
	{
		public SearchResults(ICollection<string> schemas, ICollection<TResource> resources, int startIndex, int itemsPerPage, int totalResults)
		{
			this.Schemas = new List<string>(schemas);
			this.Resources = new List<TResource>(resources);
			this.StartIndex = startIndex;
			this.ItemsPerPage = itemsPerPage;
			this.TotalResults = totalResults;
		}

		public ICollection<TResource> Resources { get; }

		public int StartIndex { get; }

		public int ItemsPerPage { get; }

		public int TotalResults { get; }

		public ICollection<string> Schemas { get; }

		public override string ToString()
		{
			var builder = new StringBuilder("SearchResults[schemas:");
			builder.Append(this.Schemas);
			builder.Append("; total:");
			builder.Append(this.TotalResults);
			builder.Append("; count:");
			builder.Append(this.Resources.Count);
			builder.Append("; index:");
			builder.Append(this.StartIndex);
			builder.Append("; resources:");
			builder.Append(this.Resources);
			builder.Append("; id:");
			builder.Append(this.GetHashCode());
			builder.Append(";]");
			return builder.ToString();
		}
	}
}