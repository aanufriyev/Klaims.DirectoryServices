namespace Klaims.Scim.DataAccess
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;

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
			throw new System.NotImplementedException();
		}

		public ScimUser GetById(string id)
		{
			throw new System.NotImplementedException();
		}

		public ScimUser Create(ScimUser resource)
		{
			throw new System.NotImplementedException();
		}

		public ScimUser Update(string id, ScimUser resource)
		{
			throw new System.NotImplementedException();
		}

		public ScimUser Remove(string id, int version)
		{
			throw new System.NotImplementedException();
		}

		public ScimUser CreateUser(ScimUser user, string password)
		{
			throw new System.NotImplementedException();
		}

		public void ChangePassword(string id, string oldPassword, string newPassword)
		{
			throw new System.NotImplementedException();
		}

		public ScimUser VerifyUser(string id, int version)
		{
			throw new System.NotImplementedException();
		}

		private IList<T> QueryInternal<T>(FilterNode filter, DbContext context) where T : class
		{
			Expression<Func<T, bool>> mainPredicate = PredicateBuilder.True<T>();
			Expression<Func<T, bool>> filterPredicate = BuildExpression<T>(filter, null, null);

			// Need to compile this stuff or fallback to direct sql generation;
			mainPredicate = mainPredicate.And(filterPredicate);
			
			return context.Set<T>().Where(mainPredicate).ToList();
		}
		// TODO: Move to query converter
		private Expression<Func<T, bool>> BuildExpression<T>(FilterNode filter, IAttributeNameMapper mapper, string prefix) where T : class
		{

			var branchNode = filter as BranchNode;
			if (branchNode != null)
			{
				if (filter.Operator.Equals(Operator.And))
				{
					var leftNodeExpression = BuildExpression<T>(branchNode.LeftNode, mapper, prefix);
					var rightNodeExpression = BuildExpression<T>(branchNode.RightNode, mapper, prefix);
					return PredicateBuilder.False<T>().And(leftNodeExpression).And(rightNodeExpression);
				}
				if (filter.Operator.Equals(Operator.Or))
				{
					var leftNodeExpression = BuildExpression<T>(branchNode.LeftNode, mapper, prefix);
					var rightNodeExpression = BuildExpression<T>(branchNode.RightNode, mapper, prefix);
					return PredicateBuilder.False<T>().Or(leftNodeExpression).Or(rightNodeExpression);
				}
				throw new InvalidOperationException("Unsupported branch operator");
			}

			var terminalNode = filter as TerminalNode;
			if (terminalNode != null)
			{
				if (filter.Operator.Equals(Operator.Eq))
				{
					return BuildEqualsExpression<T>(terminalNode.Attribute, terminalNode.Value);
				}

				if (filter.Operator.Equals(Operator.Eq))
				{
					return BuildContainsExpression<T>(terminalNode.Attribute, terminalNode.Value);
				}

				if (filter.Operator.Equals(Operator.Gt))
				{
					// Create an expression tree that represents the expression 'company.Length > 16'.
					ParameterExpression parameter = Expression.Parameter(typeof(T));
					MemberExpression property = Expression.Property(parameter, terminalNode.Attribute);
					BinaryExpression equalsExpr = Expression.GreaterThan(property, Expression.Convert(Expression.Constant(terminalNode.Value), property.Type));
					return Expression.Lambda<Func<T, bool>>(equalsExpr, parameter);
				}
				throw new InvalidOperationException("Unsupported node operator");
			}

			throw new InvalidOperationException("Unknown node type");


			//switch (filter.Operator)
			//{
			//	case PRESENCE:
			//		return getAttributeName(filter, mapper) + " IS NOT NULL";
			//	case GREATER_OR_EQUAL:
			//		return comparisonClause(filter, ">=", values, "", "", paramPrefix);
			//	case LESS_THAN:
			//		return comparisonClause(filter, "<", values, "", "", paramPrefix);
			//	case LESS_OR_EQUAL:
			//		return comparisonClause(filter, "<=", values, "", "", paramPrefix);
			//}
			//return null;

		}

		public Expression<Func<T, bool>> BuildEqualsExpression<T>(string property, object value) where T : class
		{
			ParameterExpression p = Expression.Parameter(typeof(T));
			MemberExpression propertyExpression = Expression.Property(p, property);

			// Workaround for missing coersion between String and Guid types.
			if (propertyExpression.Type == typeof(Guid))
			{
				value = Guid.Parse(value.ToString());
			}
			BinaryExpression equalsExpression = Expression.Equal(propertyExpression, Expression.Convert(Expression.Constant(value), propertyExpression.Type));
			return Expression.Lambda<Func<T, bool>>(equalsExpression, p);
		}

		private Expression<Func<T, bool>> BuildContainsExpression<T>(string propertyName, string propertyValue)
		{
			ParameterExpression parameterExp = Expression.Parameter(typeof(T), "type");
			MemberExpression propertyExp = Expression.Property(parameterExp, propertyName);
			MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
			ConstantExpression someValue = Expression.Constant(propertyValue, typeof(string));
			MethodCallExpression containsMethodExp = Expression.Call(propertyExp, method, someValue);
			return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
		}


	}
}