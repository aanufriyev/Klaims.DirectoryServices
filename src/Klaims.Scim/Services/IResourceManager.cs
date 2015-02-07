namespace Klaims.Scim.Services
{
	public interface IResourceManager<TResource>
		where TResource : class
	{
		TResource GetById(string id);

		TResource Create(TResource resource);

		TResource Update(string id, TResource resource);

		TResource Remove(string id, int version);
	}
}