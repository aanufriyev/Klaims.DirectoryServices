namespace Klaims.Scim
{
	public interface IAttributeNameMapper
	{
		string MapToInternal(string attr);

		string[] MapToInternal(string[] attr);

		string MapFromInternal(string attr);

		string[] MapFromInternal(string[] attr);
	}
}