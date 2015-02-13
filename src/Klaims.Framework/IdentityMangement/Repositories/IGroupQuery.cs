namespace Klaims.Framework.IdentityMangement
{
	using System.Linq;

	public interface IGroupQuery<TGroup> : IGroupRepository<TGroup>
		where TGroup : class
	{
		IQueryable<TGroup> Groups { get; }
	}
}