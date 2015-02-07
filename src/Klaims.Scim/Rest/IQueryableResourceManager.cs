namespace Klaims.Scim.Rest
{
	using Klaims.Scim.Query;

	public interface IQueryableResourceManager<TResource> : IResourceManager<TResource> where TResource : class
	{
		SearchResults<TResource> Query(string filter);

		SearchResults<TResource> Query(string filter, int skip, int count);
	}
}