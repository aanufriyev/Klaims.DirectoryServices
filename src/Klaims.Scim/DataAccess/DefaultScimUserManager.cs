namespace Klaims.Scim.DataAccess
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	using Klaims.Framework.Utility;
	using Klaims.Scim.Models;
	using Klaims.Scim.Query;
	using Klaims.Scim.Query.Filter;

	using Microsoft.Data.Entity;

	public class DefaultScimUserManager : IScimUserManager
	{
		private readonly ISearchQueryConverter searchQueryConverter;

		public DefaultScimUserManager(ISearchQueryConverter searchQueryConverter)
		{
			this.searchQueryConverter = searchQueryConverter;
		}

		public SearchResults<ScimUser> Query(string filter)
		{
			var processedFilter = searchQueryConverter.Convert(filter, null, true);
			return null;
		}

		public SearchResults<ScimUser> Query(string filter, int skip, int count)
		{
			throw new NotImplementedException();
		}

		public ScimUser GetById(string id)
		{
			throw new NotImplementedException();
		}

		public ScimUser Create(ScimUser resource)
		{
			throw new NotImplementedException();
		}

		public ScimUser Update(string id, ScimUser resource)
		{
			throw new NotImplementedException();
		}

		public ScimUser Remove(string id, int version)
		{
			throw new NotImplementedException();
		}

		public ScimUser CreateUser(ScimUser user, string password)
		{
			throw new NotImplementedException();
		}

		public void ChangePassword(string id, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public ScimUser VerifyUser(string id, int version)
		{
			throw new NotImplementedException();
		}

		private IList<T> QueryInternal<T>(FilterNode filter, DbContext context) where T : class
		{
			// TODO: Need to compile this stuff or fallback to direct sql generation;
			var filterPredicate = BuildExpression<T>(filter, null, null);
			return context.Set<T>().Where(filterPredicate).ToList();
		}

		// TODO: Move to query converter
		private Expression<Func<T, bool>> BuildExpression<T>(FilterNode filter, IAttributeNameMapper mapper, string prefix) where T : class
		{
			var branchNode = filter as BranchNode;
			if (branchNode != null)
			{
				var leftNodeExpression = BuildExpression<T>(branchNode.LeftNode, mapper, prefix);
				var rightNodeExpression = BuildExpression<T>(branchNode.RightNode, mapper, prefix);
				if (filter.Operator.Equals(Operator.And))
				{
					return PredicateBuilder.False<T>().And(leftNodeExpression).And(rightNodeExpression);
				}
				if (filter.Operator.Equals(Operator.Or))
				{
					return PredicateBuilder.False<T>().Or(leftNodeExpression).Or(rightNodeExpression);
				}
				throw new InvalidOperationException("Unsupported branch operator");
			}

			var terminalNode = filter as TerminalNode;
			if (terminalNode != null)
			{
				var parameter = Expression.Parameter(typeof(T));
				var property = Expression.Property(parameter, terminalNode.Attribute);

				Expression expression = null;
				if (filter.Operator.Equals(Operator.Eq))
				{
					// Workaround for missing coersion between String and Guid types.
					var propertyValue = property.Type == typeof(Guid) ? (object)Guid.Parse(terminalNode.Value) : terminalNode.Value;
					expression = Expression.Equal(property, Expression.Convert(Expression.Constant(propertyValue), property.Type));
				}

				else if (filter.Operator.Equals(Operator.Eq))
				{
					var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					expression = Expression.Call(property, method, Expression.Constant(terminalNode.Value, typeof(string)));
				}

				else if (filter.Operator.Equals(Operator.Gt))
				{
					expression = Expression.GreaterThan(property, Expression.Convert(Expression.Constant(terminalNode.Value), property.Type));
				}
				else if (filter.Operator.Equals(Operator.Ge))
				{
					expression = Expression.GreaterThanOrEqual(property, Expression.Convert(Expression.Constant(terminalNode.Value), property.Type));
				}

				else if (filter.Operator.Equals(Operator.Lt))
				{
					expression = Expression.LessThan(property, Expression.Convert(Expression.Constant(terminalNode.Value), property.Type));
				}
				else if (filter.Operator.Equals(Operator.Le))
				{
					expression = Expression.LessThanOrEqual(property, Expression.Convert(Expression.Constant(terminalNode.Value), property.Type));
				}

				else if (filter.Operator.Equals(Operator.Pr))
				{
					expression = Expression.NotEqual(property, Expression.Constant(null, property.Type));
					
				}
				if (expression == null)
				{
					throw new InvalidOperationException("Unsupported node operator");
				}

				return Expression.Lambda<Func<T, bool>>(expression, parameter);
				
			}

			throw new InvalidOperationException("Unknown node type");
		}
	}
}