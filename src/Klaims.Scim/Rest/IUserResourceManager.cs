namespace Klaims.Scim.Rest
{
	public interface IUserResourceManager<TUser> : IQueryableResourceManager<TUser>
		where TUser : class
	{
		TUser CreateUser(TUser user, string password);

		void ChangePassword(string id, string oldPassword, string newPassword);

		TUser VerifyUser(string id, int version);
	}
}