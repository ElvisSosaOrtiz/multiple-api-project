namespace Company3.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Request;
    using Routing;
    using ServiceContracts;

    [ApiController]
    [Route(CompanyThreeControllerRoutes.Root)]
    public class CompanyThreeController : ControllerBase
    {
        private readonly ICompanyAPIService _companyAPIService;

        public CompanyThreeController(ICompanyAPIService companyAPIService)
        {
            _companyAPIService = companyAPIService;
        }

        [HttpPost(CompanyThreeControllerRoutes.OfferQuote)]
        [Produces("application/xml")]
        public IActionResult SendDeliveryInfo([FromBody] CompanyThreeDeliveryInfoModel deliveryInfo)
        {
            if (deliveryInfo.Packages.Count == 0) return BadRequest("Should contain packages.");

            var result = _companyAPIService.GetOfferedQuote(deliveryInfo);
            return Ok(result);
        }
    }
}
