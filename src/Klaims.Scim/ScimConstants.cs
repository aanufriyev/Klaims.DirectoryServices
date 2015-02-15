namespace Klaims.Scim
{
    public class ScimConstants
    {
	    public const string MediaType = "application/json+scim";

        public class Routes
        {
            public class Templates
            {
                public const string Users = "scim/Users";
                public const string Groups = "scim/Groups";
                public const string Search = "scim/Search";
                public const string Bulk = "scim/Bulk";
                public const string Schemas = "scim/Schemas";
                public const string ResourceTypes = "scim/ResourceTypes";
                public const string Self = "scim/Self";
                public const string ServiceProviderConfig = "scim/ServiceProviderConfig";
            }

        } 
    }
}