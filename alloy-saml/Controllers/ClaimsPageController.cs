using alloy_saml.Controllers;
using alloy_saml.Models;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace alloy_saml.Controllers
{
    public class ClaimsPageController : PageControllerBase<ClaimsPage>
    {
        public ViewResult Index(ClaimsPage currentPage)
        {
            return View(new ClaimsPageViewModel(currentPage));
        }
    }
}
