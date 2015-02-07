namespace Klaims.Scim.Query
{
	using System.Collections.Generic;
	using System.Text;

	public class SearchResults<TResource>
	{
		public SearchResults(ICollection<string> schemas, ICollection<TResource> resources, int startIndex, int itemsPerPage, int totalResults)
		{
			Schemas = new List<string>(schemas);
			Resources = new List<TResource>(resources);
			StartIndex = startIndex;
			ItemsPerPage = itemsPerPage;
			TotalResults = totalResults;
		}

		public ICollection<TResource> Resources { get; }

		public int StartIndex { get; }

		public int ItemsPerPage { get; }

		public int TotalResults { get; }

		public ICollection<string> Schemas { get; }

		public override string ToString()
		{
			var builder = new StringBuilder("SearchResults[schemas:");
			builder.Append(Schemas);
			builder.Append("; total:");
			builder.Append(TotalResults);
			builder.Append("; count:");
			builder.Append(Resources.Count);
			builder.Append("; index:");
			builder.Append(StartIndex);
			builder.Append("; resources:");
			builder.Append(Resources);
			builder.Append("; id:");
			builder.Append(GetHashCode());
			builder.Append(";]");
			return builder.ToString();
		}
	}
}