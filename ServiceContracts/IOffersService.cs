namespace ServiceContracts
{
    using Models.Request;
    using Models.Response;
    using System.Threading.Tasks;

    public interface IOffersService
    {
        Task<ConsumerOfferModel> GetBestOfferAsync(ConsumerDeliveryInfoModel deliveryInfo);
    }
}
