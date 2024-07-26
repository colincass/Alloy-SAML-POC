using alloy_saml;
using alloy_saml.Models;
using alloy_saml.Models.Pages;
using alloy_saml.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace alloy_saml.Models
{
    [Authorize]
    [SiteContentType(GUID = "bb9c322d-5856-4840-8e65-a56f33767898",
        GroupName = Globals.GroupNames.Content)]
    public class ClaimsPage : SitePageData
    {
        [Display(GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual XhtmlString MainBody { get; set; }
    }

    public class ClaimsPageViewModel : PageViewModel<ClaimsPage>
    {
        public ClaimsPageViewModel(ClaimsPage currentPage)
        : base(currentPage)
        {
        }
    }
}
