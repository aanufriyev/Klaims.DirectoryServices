namespace Klaims.Scim.Services
{
	using Klaims.Scim.Query;

	public interface IQueryableResourceManager<TResource> :IResourceQuery<TResource>, IResourceManager<TResource> where TResource : class
	{
		 
	}
}