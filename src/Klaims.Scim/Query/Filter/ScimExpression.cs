namespace Klaims.Scim.Query.Filter
{
	public abstract class ScimExpression
	{
		protected ScimExpression(Operator filterOperator)
		{
			this.Operator = filterOperator;
		}

		public Operator Operator { get; private set; }
	}
}
