namespace Services
{
    using Microsoft.Extensions.Logging;
    using Models.Request;
    using Models.Response;
    using ServiceContracts;

    public class CompanyAPIService : ICompanyAPIService
    {
        private readonly ILogger<CompanyAPIService> _logger;

        public CompanyAPIService(ILogger<CompanyAPIService> logger)
        {
            _logger = logger;
        }

        public CompanyOneOfferModel GetOfferedTotal(CompanyOneDeliveryInfoModel deliveryInfo)
        {
            if (deliveryInfo.PackageDimensions.Count == 0) throw new NullReferenceException("Should contain package dimensions.");

            try
            {
                var offeredAmount = GetRandomOfferAmount(deliveryInfo.PackageDimensions);

                return new()
                {
                    CompanyName = "Company1",
                    ContactAddress = deliveryInfo.ContactAddress,
                    WarehouseAddress = deliveryInfo.WarehouseAddress,
                    OfferedTotal = offeredAmount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new();
            }
        }

        public CompanyTwoOfferModel GetOfferedAmount(CompanyTwoDeliveryInfoModel deliveryInfo)
        {
            if (deliveryInfo.Cartons.Count == 0) throw new NullReferenceException("Should contain cartons.");

            try
            {
                var offeredAmount = GetRandomOfferAmount(deliveryInfo.Cartons);

                return new()
                {
                    CompanyName = "Company2",
                    Consignee = deliveryInfo.Consignee,
                    Consignor = deliveryInfo.Consignor,
                    OfferedAmount = offeredAmount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new();
            }
        }

        public CompanyThreeOfferModel GetOfferedQuote(CompanyThreeDeliveryInfoModel deliveryInfo)
        {
            if (deliveryInfo.Packages.Count == 0) throw new NullReferenceException("Should contain packages.");

            try
            {
                var offeredAmount = GetRandomOfferAmount(deliveryInfo.Packages);

                return new()
                {
                    CompanyName = "Company3",
                    Source = deliveryInfo.Source,
                    Destination = deliveryInfo.Destination,
                    OfferedQuote = offeredAmount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new();
            }
        }

        private int GetRandomOfferAmount(List<string> cartonDimensions)
        {
            var random = new Random();

            return cartonDimensions.Count switch
            {
                >= 1 and <= 3 => random.Next(1000, 3000),
                >= 4 and <= 6 => random.Next(4000, 6000),
                >= 7 and <= 10 => random.Next(7000, 10000),
                _ => random.Next(11000, 20000)
            };
        }
    }
}
