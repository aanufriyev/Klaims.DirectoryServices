namespace Klaims.Scim.Models
{
    using System.Collections.Generic;

    public class ComplexAttribute : ResourceAttribute
    {
        public ComplexAttribute(string name, List<SimpleAttribute> attributeList)
            : base(name)
        {
            AttributeList = attributeList;
        }

        public List<SimpleAttribute> AttributeList { get; private set; }

        public override string ToString()
        {
            return string.Format("ComplexAttribute:{0}", Name);
        }
    }
}