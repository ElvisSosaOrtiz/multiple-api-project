namespace Consumer.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Request;
    using Routing;
    using ServiceContracts;

    [ApiController]
    [Route(ConsumerControllerRoutes.Root)]
    public class ConsumerController : ControllerBase
    {
        private readonly IOffersService _offersService;

        public ConsumerController(IOffersService offersService)
        {
            _offersService = offersService;
        }

        [HttpPost(ConsumerControllerRoutes.BestOffer)]
        public async Task<IActionResult> GetBestOffer([FromBody] ConsumerDeliveryInfoModel deliveryInfo)
        {
            if (deliveryInfo.CartonDimensions.Count == 0) return BadRequest("Should contain carton dimensions.");

            var result = await _offersService.GetBestOfferAsync(deliveryInfo);
            return Ok(result);
        }
    }
}
