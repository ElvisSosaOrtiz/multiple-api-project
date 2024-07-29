namespace ServiceContracts
{
    using Models.Request;
    using Models.Response;

    public interface ICompanyAPIService
    {
        CompanyOneOfferModel GetOfferedTotal(CompanyOneDeliveryInfoModel deliveryInfo);

        CompanyTwoOfferModel GetOfferedAmount(CompanyTwoDeliveryInfoModel deliveryInfo);

        CompanyThreeOfferModel GetOfferedQuote(CompanyThreeDeliveryInfoModel deliveryInfo);
    }
}
