namespace Tests.Builders
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using ServiceContracts;
    using Services;

    public class OffersServiceBuilder
    {
        private IHttpClientFactory? _httpClientFactory;
        private ILogger<OffersService>? _logger;
        private IJsonConvertWrapper? _jsonConvertWrapper;

        public OffersService Build()
        {
            var httpClientFactory = _httpClientFactory ?? Mock.Of<IHttpClientFactory>();
            var logger = _logger ?? Mock.Of<ILogger<OffersService>>();
            var jsonConvertWrapper = _jsonConvertWrapper ?? Mock.Of<IJsonConvertWrapper>();

            return new(httpClientFactory, logger, jsonConvertWrapper);
        }

        public OffersServiceBuilder With(IHttpClientFactory value)
        {
            _httpClientFactory = value;
            return this;
        }

        public OffersServiceBuilder With(ILogger<OffersService> value)
        {
            _logger = value;
            return this;
        }

        public OffersServiceBuilder With(IJsonConvertWrapper value)
        {
            _jsonConvertWrapper = value;
            return this;
        }
    }
}
