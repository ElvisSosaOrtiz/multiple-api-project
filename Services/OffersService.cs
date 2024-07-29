namespace Services
{
    using Microsoft.Extensions.Logging;
    using Models.Request;
    using Models.Response;
    using Routing;
    using ServiceContracts;
    using Services.Constants;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public class OffersService : IOffersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OffersService> _logger;
        private readonly IJsonConvertWrapper _jsonConvertWrapper;

        public OffersService(
            IHttpClientFactory httpClientFactory,
            ILogger<OffersService> logger,
            IJsonConvertWrapper jsonConvertWrapper)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _jsonConvertWrapper = jsonConvertWrapper;
        }

        public async Task<ConsumerOfferModel> GetBestOfferAsync(ConsumerDeliveryInfoModel deliveryInfo)
        {
            if (deliveryInfo.CartonDimensions.Count == 0) throw new NullReferenceException("Should contain carton dimensions.");

            try
            {
                var (companyOneOffer, companyTwoOffer, companyThreeOffer) = await MapOffersAsync(deliveryInfo);
                var offersList = new List<ConsumerOfferModel>
                {
                    new()
                    {
                        CompanyName = companyOneOffer.CompanyName,
                        SourceAddress = companyOneOffer.ContactAddress,
                        DestinationAddress = companyOneOffer.WarehouseAddress,
                        Offer = companyOneOffer.OfferedTotal
                    },
                    new()
                    {
                        CompanyName = companyTwoOffer.CompanyName,
                        SourceAddress = companyTwoOffer.Consignee,
                        DestinationAddress = companyTwoOffer.Consignor,
                        Offer = companyTwoOffer.OfferedAmount
                    },
                    new()
                    {
                        CompanyName = companyThreeOffer.CompanyName,
                        SourceAddress = companyThreeOffer.Source,
                        DestinationAddress = companyThreeOffer.Destination,
                        Offer = companyThreeOffer.OfferedQuote
                    }
                };

                var bestOffer = offersList.Min(x => x.Offer);

                return offersList.First(x => x.Offer == bestOffer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new();
            }
        }

        private async Task<(CompanyOneOfferModel CompanyOneOffer, CompanyTwoOfferModel CompanyTwoOffer, CompanyThreeOfferModel CompanyThreeOffer)> MapOffersAsync(ConsumerDeliveryInfoModel deliveryInfo)
        {
            var companyOneDeliveryInfo = new CompanyOneDeliveryInfoModel
            {
                ContactAddress = deliveryInfo.SourceAddress,
                WarehouseAddress = deliveryInfo.DestinationAddress,
                PackageDimensions = deliveryInfo.CartonDimensions
            };
            var companyOneOffer = await GetCompanyOneOfferAsync(companyOneDeliveryInfo);

            var companyTwoDeliveryInfo = new CompanyTwoDeliveryInfoModel
            {
                Consignee = deliveryInfo.SourceAddress,
                Consignor = deliveryInfo.DestinationAddress,
                Cartons = deliveryInfo.CartonDimensions
            };
            var companyTwoOffer = await GetCompanyTwoOfferAsync(companyTwoDeliveryInfo);

            var companyThreeDeliveryInfo = new CompanyThreeDeliveryInfoModel
            {
                Source = deliveryInfo.SourceAddress,
                Destination = deliveryInfo.DestinationAddress,
                Packages = deliveryInfo.CartonDimensions
            };
            var companyThreeOffer = await GetCompanyThreeOfferAsync(companyThreeDeliveryInfo);

            return (companyOneOffer, companyTwoOffer, companyThreeOffer)!;
        }

        private async Task<CompanyOneOfferModel?> GetCompanyOneOfferAsync(CompanyOneDeliveryInfoModel deliveryInfo)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(HttpClientNames.CompanyOne);

                var response = await httpClient.PostAsJsonAsync(CompanyOneControllerRoutes.GetOfferTotal, deliveryInfo);
                var responseAsString = await response.Content.ReadAsStringAsync();

                return _jsonConvertWrapper.DeserializeObject<CompanyOneOfferModel>(responseAsString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new();
            }
        }

        private async Task<CompanyTwoOfferModel?> GetCompanyTwoOfferAsync(CompanyTwoDeliveryInfoModel deliveryInfo)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(HttpClientNames.CompanyTwo);

                var response = await httpClient.PostAsJsonAsync(CompanyTwoControllerRoutes.GetOfferAmount, deliveryInfo);
                var responseAsString = await response.Content.ReadAsStringAsync();

                return _jsonConvertWrapper.DeserializeObject<CompanyTwoOfferModel>(responseAsString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new();
            }
        }

        public async Task<CompanyThreeOfferModel?> GetCompanyThreeOfferAsync(CompanyThreeDeliveryInfoModel deliveryInfo)
        {
            try
            {
                //Serialize object received from argument as XML string.
                var stringWriter = new StringWriter();
                var inputSerializer = new XmlSerializer(typeof(CompanyThreeDeliveryInfoModel));
                inputSerializer.Serialize(stringWriter, deliveryInfo);

                //Prepare content and send request.
                var stringContent = new StringContent(stringWriter.ToString(), stringWriter.Encoding, "application/xml");
                var httpClient = _httpClientFactory.CreateClient(HttpClientNames.CompanyThree);
                var response = await httpClient.PostAsync(CompanyThreeControllerRoutes.GetOfferQuote, stringContent);
                var responseAsString = await response.Content.ReadAsStringAsync();

                //Deserialize XML string as return type reference.
                TextReader reader = new StringReader(responseAsString);
                var outputSerializer = new XmlSerializer(typeof(CompanyThreeOfferModel));

                return (CompanyThreeOfferModel)outputSerializer.Deserialize(reader)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new();
            }
        }
    }
}
