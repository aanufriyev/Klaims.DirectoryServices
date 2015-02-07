namespace Klaims.Scim.Query
{
	public interface IResourceQuery<TResource> where TResource : class
	{
		SearchResults<TResource> Query(string filter);
		SearchResults<TResource> Query(string filter, int skip, int count);
	}
}