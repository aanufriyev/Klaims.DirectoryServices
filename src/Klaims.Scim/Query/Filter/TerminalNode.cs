namespace Klaims.Scim.Query.Filter
{
	public class TerminalNode : FilterNode
	{
		#region Constructors and Destructors

		public TerminalNode(Operator filterOperation)
			: base(filterOperation)
		{
		}

		#endregion

		#region Public Properties

		public string Attribute { get; set; }

		public string Value { get; set; }

		#endregion

		#region Public Methods and Operators

		public override string ToString()
		{
			return string.Format("({0} {1} {2})", this.Attribute, this.Operator, this.Value);
		}

		#endregion
	}
}