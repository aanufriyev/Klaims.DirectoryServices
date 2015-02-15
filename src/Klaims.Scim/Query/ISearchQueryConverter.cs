namespace Klaims.Scim.Query
{
	using System;
	using System.Linq.Expressions;

	using Klaims.Scim.Services;

	public interface ISearchQueryConverter<T> where T : class
	{
		Expression<Func<T, bool>> Convert(string filter, string sortBy, bool ascending, IAttributeNameMapper mapper = null);
	}
}