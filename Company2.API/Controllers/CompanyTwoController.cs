namespace Company2.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Request;
    using Routing;
    using ServiceContracts;

    [ApiController]
    [Route(CompanyTwoControllerRoutes.Root)]
    public class CompanyTwoController : ControllerBase
    {
        private readonly ICompanyAPIService _companyAPIService;

        public CompanyTwoController(ICompanyAPIService companyAPIService)
        {
            _companyAPIService = companyAPIService;
        }

        [HttpPost(CompanyTwoControllerRoutes.OfferAmount)]
        public IActionResult SendDeliveryInfo([FromBody] CompanyTwoDeliveryInfoModel deliveryInfo)
        {
            if (deliveryInfo.Cartons.Count == 0) return BadRequest("Should contain cartons.");

            var result = _companyAPIService.GetOfferedAmount(deliveryInfo);
            return Ok(result);
        }
    }
}
