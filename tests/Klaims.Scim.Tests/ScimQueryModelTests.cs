﻿namespace Klaims.Scim.Tests
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

		private readonly List<UserAccount> testUsers = new List<UserAccount>
			                                        {
				                                        new UserAccount { ProfileUrl = "http://myprofile.com", DisplayName = "BestEmployee" },
														new UserAccount { ProfileUrl = "http://myprofile.com", DisplayName = "SomeEmployee" },
                                                        new UserAccount { DisplayName = "BestEmployee" }
			                                        };

		[Fact]
		public void CanConvertBasicFilter()
		{
			var mapperMoq = new Mock<IAttributeNameMapper>();
			mapperMoq.Setup(m => m.MapToInternal(It.IsAny<string>())).Returns((string s) => char.ToUpper(s[0]) + s.Substring(1));

			var filterNode = UriFilterExpressionParser.Parse(BasicFilter);
			var converter = new DefaultFilterBinder();
			var predicate = converter.Bind<UserAccount>(filterNode, string.Empty, false, mapperMoq.Object);
			Assert.NotNull(predicate);

			var usersCount = this.testUsers.AsQueryable().Count(predicate);
			Assert.Equal(2, usersCount);
		}
	}
}