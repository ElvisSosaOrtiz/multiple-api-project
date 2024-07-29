namespace Tests
{
    using Models.Request;
    using Models.Response;
    using Moq;
    using Moq.Protected;
    using ServiceContracts;
    using Services.Constants;
    using Tests.Builders;

    [TestFixture]
    public class OffersServiceTests
    {
        public class GetBestOfferAsyncTests
        {
            [Test]
            public void WhenCartonDimensionsIsEmpty_ShouldThrowNullReferenceException()
            {
                var expectedDeliveryInfo = new ConsumerDeliveryInfoModel
                {
                    SourceAddress = "source-address",
                    DestinationAddress = "destination-address",
                    CartonDimensions = []
                };
                var instance = new OffersServiceBuilder().Build();

                Assert.ThrowsAsync<NullReferenceException>(() => instance.GetBestOfferAsync(expectedDeliveryInfo), "Should contain carton dimensions.");
            }

            [TestCase("Company1", 3000, 4000, 5000)]
            [TestCase("Company2", 5000, 3000, 4000)]
            [TestCase("Company3", 5000, 4000, 3000)]
            public async Task ShouldReturnBestOffer(string companyName, double offeredTotal, double offeredAmount, double offeredQuote)
            {
                const string
                    SourceAddress = nameof(SourceAddress),
                    DestinationAddress = nameof(DestinationAddress);
                var cartonDimensions = new List<string> { "a", "b", "c", "d" };
                var expectedDeliveryInfo = new ConsumerDeliveryInfoModel
                {
                    SourceAddress = SourceAddress,
                    DestinationAddress = DestinationAddress,
                    CartonDimensions = cartonDimensions
                };
                var companyOneOfferModel = new CompanyOneOfferModel
                {
                    CompanyName = "Company1",
                    ContactAddress = SourceAddress,
                    WarehouseAddress = DestinationAddress,
                    OfferedTotal = offeredTotal,
                };
                var companyTwoOfferModel = new CompanyTwoOfferModel
                {
                    CompanyName = "Company2",
                    Consignee = SourceAddress,
                    Consignor = DestinationAddress,
                    OfferedAmount = offeredAmount
                };
                var companyThreeOfferModel = new CompanyThreeOfferModel
                {
                    CompanyName = "Company3",
                    Source = SourceAddress,
                    Destination = DestinationAddress,
                    OfferedQuote = offeredQuote
                };
                var jsonConvertWrapperMock = new Mock<IJsonConvertWrapper>();
                jsonConvertWrapperMock
                    .Setup(mock => mock.DeserializeObject<CompanyOneOfferModel>(It.IsAny<string>()))
                    .Returns(companyOneOfferModel);
                jsonConvertWrapperMock
                    .Setup(mock => mock.DeserializeObject<CompanyTwoOfferModel>(It.IsAny<string>()))
                    .Returns(companyTwoOfferModel);
                var instance = new OffersServiceBuilder()
                    .With(jsonConvertWrapperMock.Object)
                    .With(MockHttpClientFactory(SourceAddress, DestinationAddress, offeredQuote))
                    .Build();

                var actual = await instance.GetBestOfferAsync(expectedDeliveryInfo);

                Assert.That(actual.CompanyName, Is.EqualTo(companyName));
            }

            private IHttpClientFactory MockHttpClientFactory(string sourceAddress, string destinationAddress, double offeredQuote)
            {
                const string
                    CompanyOneBaseURL = "https://company1.test/",
                    CompanyTwoBaseURL = "https://comany2.test/",
                    CompanyThreeBaseURL = "https://company3.test/";
                var expectedXmlString = $"<CompanyThreeOfferModel><CompanyName>Company3</CompanyName><Source>{sourceAddress}</Source><Destination>{destinationAddress}</Destination><OfferedQuote>{offeredQuote}</OfferedQuote></CompanyThreeOfferModel>";
                var companyOneHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                var companyOneResponseMessage = new HttpResponseMessage();
                companyOneHandlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(companyOneResponseMessage)
                    .Verifiable();
                var companyOneHttpClient = new HttpClient(companyOneHandlerMock.Object) { BaseAddress = new Uri(CompanyOneBaseURL) };

                var companyTwoHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                var companyTwoResponseMessage = new HttpResponseMessage();
                companyTwoHandlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(companyTwoResponseMessage)
                    .Verifiable();
                var companyTwoHttpClient = new HttpClient(companyTwoHandlerMock.Object) { BaseAddress = new Uri(CompanyTwoBaseURL) };

                var companyThreeHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                var companyThreeResponseMessage = new HttpResponseMessage() { Content = new StringContent(expectedXmlString) };
                companyThreeHandlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(companyThreeResponseMessage)
                    .Verifiable();
                var companyThreeHttpClient = new HttpClient(companyThreeHandlerMock.Object) { BaseAddress = new Uri(CompanyThreeBaseURL) };

                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock
                    .Setup(mock => mock.CreateClient(HttpClientNames.CompanyOne))
                    .Returns(companyOneHttpClient);
                httpClientFactoryMock
                    .Setup(mock => mock.CreateClient(HttpClientNames.CompanyTwo))
                    .Returns(companyTwoHttpClient);
                httpClientFactoryMock
                    .Setup(mock => mock.CreateClient(HttpClientNames.CompanyThree))
                    .Returns(companyThreeHttpClient);

                return httpClientFactoryMock.Object;
            }
        }
    }
}
