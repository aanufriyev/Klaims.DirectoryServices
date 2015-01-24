namespace Klaims.Scim.Models
{
    public class SimpleAttribute :ResourceAttribute
    {
	    public SimpleAttribute(string name)
		    : base(name)
	    {
		    
	    }
        public object Value { get; set; }
    }
}