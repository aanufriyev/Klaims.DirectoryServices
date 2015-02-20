namespace Klaims.Scim.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Klaims.Framework.IdentityMangement.Models;
	using Klaims.Scim.Query;
	using Klaims.Scim.Query.Filter;
	using Klaims.Scim.Services;

	using Moq;

	using Xunit;

	public class ScimQueryModelTests
	{
		private const string BasicFilter = "profileUrl pr and displayName co \"Employee\"";

		private readonly List<User> testUsers = new List<User>
			                                        {
				                                        new User { ProfileUrl = "http://myprofile.com", DisplayName = "BestEmployee" },
														new User { ProfileUrl = "http://myprofile.com", DisplayName = "SomeEmployee" },
                                                        new User { DisplayName = "BestEmployee" }
			                                        };

		[Fact]
		public void CanConvertBasicFilter()
		{
			var mapperMoq = new Mock<IAttributeNameMapper>();
			mapperMoq.Setup(m => m.MapToInternal(It.IsAny<string>())).Returns((string s) => char.ToUpper(s[0]) + s.Substring(1));

			var filterNode = UriFilterExpressionParser.Parse(BasicFilter);
			var converter = new DefaultFilterBinder();
			var predicate = converter.Bind<User>(filterNode, string.Empty, false, mapperMoq.Object);
			Assert.NotNull(predicate);

			var usersCount = this.testUsers.AsQueryable().Count(predicate);
			Assert.Equal(2, usersCount);
		}
	}
}