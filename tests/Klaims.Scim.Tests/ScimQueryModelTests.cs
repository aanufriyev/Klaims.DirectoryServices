namespace Klaims.Scim.Tests
{
	using System;

	using Klaims.Framework.IdentityMangement.Models;
	using Klaims.Scim.Query;
	using Klaims.Scim.Query.Filter;
	using Klaims.Scim.Services;
	using Klaims.Scim.Tests.Models;

	using Moq;

	using Xunit;

	public class ScimQueryModelTests
	{
		private const string BasicFilter = "profileUrl pr and displayName co \"Employee\"";

		[Fact]
		public void CanConvertBasicFilter()
		{
			var mapperMoq = new Mock<IAttributeNameMapper>();
			mapperMoq.Setup(m => m.MapToInternal(It.IsAny<string>())).Returns((string s) => char.ToUpper(s[0]) + s.Substring(1));

			var converter = new ScimSearchQueryConverter<User>();
			var predicate = converter.Convert(BasicFilter, string.Empty, false, mapperMoq.Object);
			Assert.NotNull(predicate);
			var translator = new TestExpressionVisitor();
			string whereClause = translator.Translate(predicate);
			Console.WriteLine(whereClause);
			Console.WriteLine(predicate);
		}
	}
}