namespace Klaims.Scim.DataAccess
{
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public interface ISearchQueryConverter
	{
		SqlFilter Convert(string filter, string sortBy, bool ascending, IAttributeNameMapper mapper = null);
	}
	
	public class SqlFilter
	{
		public SqlFilter(string sql, Dictionary<string, object> parameters)
		{
			Sql = sql;
			Parameters = parameters;
		}

		public string Sql { get; }

		public Dictionary<string, object> Parameters { get; }

		public string ParameterPrefix { get; set; }

		public override string ToString()
		{
			return string.Format("Sql: {0}, Parameters: {1}", Sql, Parameters);
		}
	}
}