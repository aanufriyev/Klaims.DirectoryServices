namespace Klaims.Scim.Query.Filter
{
	using System;
	using System.Text;

	public class BranchNode : FilterNode
	{
		#region Constructors and Destructors

		public BranchNode(Operator filterOperator)
			: base(filterOperator)
		{
		}

		#endregion

		#region Public Properties

		public bool HasBothChildren => LeftNode != null && RightNode != null;

		public FilterNode LeftNode { get; private set; }

		public FilterNode RightNode { get; private set; }

		#endregion

		#region Public Methods and Operators

		public void AddNode(FilterNode node)
		{
			if (LeftNode == null)
			{
				LeftNode = node;
			}
			else if (RightNode == null)
			{
				RightNode = node;
			}
			else
			{
				throw new InvalidOperationException("A branch node can only hold two nodes");
			}
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			if (LeftNode != null)
			{
				sb.Append(LeftNode);
			}

			sb.Append(Operator.Equals(Operator.And) ? " AND " : " OR ");

			if (RightNode != null)
			{
				sb.Append(RightNode);
			}

			return sb.ToString();
		}

		#endregion
	}
}