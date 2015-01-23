namespace Klaims.Scim.Models
{
    using System.Collections.Generic;

    public class Resource
    {
        public Dictionary<string, List<ResourceAttribute>> Attributes { get; } = new Dictionary<string, List<ResourceAttribute>>();

        public string Id { get; set; }

        public void AddAttribute(string uri, ResourceAttribute attribute)
        {
            var atttributeList = Attributes[uri];

            if (atttributeList == null)
            {
                atttributeList = new List<ResourceAttribute>();
                Attributes.Add(uri, atttributeList);
            }

            atttributeList.Add(attribute);
        }

        public ResourceAttribute Get(string name)
        {
            foreach (var attributeList in Attributes.Values)
            {
                foreach (var attribute in attributeList)
                {
                    if (attribute.Name.Equals(name))
                    {
                        return attribute;
                    }
                }
            }

            return null;
        }

        public T GetValue<T>(string name)
        {
            var attribute = Get(name);

            if (attribute == null)
            {
                return default(T);
            }
            var simpleAttribute = attribute as SimpleAttribute;
            return simpleAttribute != null ? (T)simpleAttribute.Value : default(T);
        }
    }
}