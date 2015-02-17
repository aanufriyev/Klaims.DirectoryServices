namespace Klaims.Scim.Query
{
	using System;
	using System.Linq.Expressions;

	using Klaims.Framework.IdentityMangement.Models;
	using Klaims.Framework.Utility;
	using Klaims.Scim.Query.Filter;
	using Klaims.Scim.Services;

	public class ScimSearchQueryConverter<T> : ISearchQueryConverter<T>
		where T : User
	{
		public Expression<Func<T, bool>> Convert(string filter, string sortBy, bool ascending, IAttributeNameMapper mapper = null)
		{
			var filterNode = ScimFilterParser.Parse(filter);
			return this.BuildExpression(filterNode, mapper, null);
		}

		private Expression<Func<T, bool>> BuildExpression(FilterNode filter, IAttributeNameMapper mapper, string prefix)
		{
			var branchNode = filter as BranchNode;
			if (branchNode != null)
			{
				var leftNodeExpression = this.BuildExpression(branchNode.Left, mapper, prefix);
				var rightNodeExpression = this.BuildExpression(branchNode.Right, mapper, prefix);
				if (filter.Operator.Equals(Operator.And))
				{
					return leftNodeExpression.And(rightNodeExpression);
				}
				if (filter.Operator.Equals(Operator.Or))
				{
					return leftNodeExpression.Or(rightNodeExpression);
				}
				throw new InvalidOperationException("Unsupported branch operator");
			}

			var terminalNode = filter as TerminalNode;
			if (terminalNode != null)
			{
				var parameter = Expression.Parameter(typeof(T));
				var property = Expression.Property(parameter, mapper.MapToInternal(terminalNode.Attribute));

				Expression expression = null;
				if (filter.Operator.Equals(Operator.Eq))
				{
					// Workaround for missing coersion between String and Guid types.
					var propertyValue = property.Type == typeof(Guid) ? (object)Guid.Parse(terminalNode.Value) : terminalNode.Value;
					expression = Expression.Equal(property, Expression.Convert(Expression.Constant(propertyValue), property.Type));
				}
				else if (filter.Operator.Equals(Operator.Co))
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