namespace Company1.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Request;
    using Routing;
    using ServiceContracts;

    [ApiController]
    [Route(CompanyOneControllerRoutes.Root)]
    public class CompanyOneController : ControllerBase
    {
        private readonly ICompanyAPIService _companyAPIService;

        public CompanyOneController(ICompanyAPIService companyAPIService)
        {
            _companyAPIService = companyAPIService;
        }

        [HttpPost(CompanyOneControllerRoutes.OfferTotal)]
        public IActionResult SendDeliveryInfo([FromBody] CompanyOneDeliveryInfoModel deliveryInfo)
        {
            if (deliveryInfo.PackageDimensions.Count == 0) return BadRequest("Should contain package dimensions.");

            var result = _companyAPIService.GetOfferedTotal(deliveryInfo);
            return Ok(result);
        }
    }
}
