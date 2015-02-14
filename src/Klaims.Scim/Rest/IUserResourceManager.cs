namespace Klaims.Scim.Rest
{
	using Klaims.Scim.Resources;

	public interface IUserResourceManager<TUser> : IQueryableResourceManager<TUser>
		where TUser : ScimUser
	{
		TUser CreateUser(TUser user, string password);

		void ChangePassword(string id, string oldPassword, string newPassword);

		TUser VerifyUser(string id, int version);
	}
}