using ITfoxtec.Identity.Saml2.Claims;
using System.Security.Claims;

namespace alloy_saml.Helpers
{
    // Only needed in scenarios where additional claims are fetched from an external source or similar
    public static class ClaimsTransform
    {
        public static ClaimsPrincipal Transform(ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return incomingPrincipal;
            }

            return CreateClaimsPrincipal(incomingPrincipal);
        }

        private static ClaimsPrincipal CreateClaimsPrincipal(ClaimsPrincipal incomingPrincipal)
        {
            var claims = new List<Claim>();

            // All claims
            claims.AddRange(incomingPrincipal.Claims);

            // Or custom claims            
            claims.AddRange(GetSaml2LogoutClaims(incomingPrincipal));
            //claims.Add(new Claim(ClaimTypes.NameIdentifier, GetClaimValue(incomingPrincipal, ClaimTypes.NameIdentifier)));

            // For this POC we will just add a static role claim, but in a real implementation you would configure roles in the IDP, which can vary by vendor
            if (incomingPrincipal.Identity.IsAuthenticated)
            {
                if (incomingPrincipal.Identity.Name.Equals("admin@example.com", StringComparison.OrdinalIgnoreCase))                
                {
                    claims.Add(new Claim(ClaimTypes.Role, "CmsAdmins", ClaimTypes.Role));
                }
            }

            return new ClaimsPrincipal(new ClaimsIdentity(claims, incomingPrincipal.Identity.AuthenticationType, ClaimTypes.NameIdentifier, ClaimTypes.Role)
            {
                BootstrapContext = ((ClaimsIdentity)incomingPrincipal.Identity).BootstrapContext
            });
        }

        private static IEnumerable<Claim> GetSaml2LogoutClaims(ClaimsPrincipal principal)
        {
            yield return GetClaim(principal, Saml2ClaimTypes.NameId);
            yield return GetClaim(principal, Saml2ClaimTypes.NameIdFormat);
            yield return GetClaim(principal, Saml2ClaimTypes.SessionIndex);
        }
        private static Claim GetClaim(ClaimsPrincipal principal, string claimType)
        {
            return ((ClaimsIdentity)principal.Identity).Claims.Where(c => c.Type == claimType).FirstOrDefault();
        }

        private static string GetClaimValue(ClaimsPrincipal principal, string claimType)
        {
            var claim = GetClaim(principal, claimType);
            return claim != null ? claim.Value : null;
        }
    }
}
