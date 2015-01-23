namespace Klaims.Scim.Models
{
    public class ResourceAttribute
    {
        protected ResourceAttribute(string name)
        {
            this.Name = Name;
        }

        public string Name { get; set; }
    }
}