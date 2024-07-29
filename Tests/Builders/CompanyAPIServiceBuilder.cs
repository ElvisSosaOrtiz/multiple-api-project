namespace Tests.Builders
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using Services;

    public class CompanyAPIServiceBuilder
    {
        private ILogger<CompanyAPIService>? _logger;

        public CompanyAPIService Build()
        {
            var logger = _logger ?? Mock.Of<ILogger<CompanyAPIService>>();

            return new(logger);
        }

        public CompanyAPIServiceBuilder With(ILogger<CompanyAPIService> value)
        {
            _logger = value;
            return this;
        }
    }
}
