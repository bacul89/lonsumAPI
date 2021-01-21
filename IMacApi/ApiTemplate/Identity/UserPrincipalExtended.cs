using System.DirectoryServices.AccountManagement;

namespace ApiTemplate.Identity
{
    public class UserPrincipalExSearchFilter : AdvancedFilters
    {
        public UserPrincipalExSearchFilter(Principal p) : base(p) { }

        public void LogonCount(int value, MatchType mt)
        {
            this.AdvancedFilterSet("LogonCount", value, typeof(int), mt);
        }
    }

    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("User")]
    public class UserPrincipalExtended:UserPrincipal
    {
        public UserPrincipalExtended(PrincipalContext context) : base(context)
        {
        }

        public UserPrincipalExtended(PrincipalContext context, string samAccountName, string password, bool enabled) :
            base(context, samAccountName, password, enabled)
        {
        }

        UserPrincipalExSearchFilter _searchFilter;

        public new UserPrincipalExSearchFilter AdvancedSearchFilter =>
            _searchFilter ?? (_searchFilter = new UserPrincipalExSearchFilter(this));

        // Create the "Title" property.    
        [DirectoryProperty("title")]
        public string Title
        {
            get
            {
                if (ExtensionGet("title").Length != 1)
                    return string.Empty;

                return (string)ExtensionGet("title")[0];
            }
            set => ExtensionSet("title", value);
        }

        // Implement the overloaded search method FindByIdentity.
        public new static UserPrincipalExtended FindByIdentity(PrincipalContext context, string identityValue)
        {
            return (UserPrincipalExtended) FindByIdentityWithType(context, typeof(UserPrincipalExtended),
                identityValue);
        }

        // Implement the overloaded search method FindByIdentity. 
        public new static UserPrincipalExtended FindByIdentity(PrincipalContext context, IdentityType identityType,
            string identityValue)
        {
            return (UserPrincipalExtended) FindByIdentityWithType(context, typeof(UserPrincipalExtended), identityType,
                identityValue);
        }
    }
}