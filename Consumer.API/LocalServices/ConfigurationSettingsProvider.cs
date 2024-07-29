namespace Consumer.API.LocalServices
{
    using ServiceContracts;

    public class ConfigurationSettingsProvider : IConfigurationSettingsProvider
    {
        private readonly IConfiguration _configuration;

        public ConfigurationSettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CompanyOneBaseUrl => _configuration.GetValue<string>("CompanyOneBaseUrl")!;

        public string CompanyTwoBaseUrl => _configuration.GetValue<string>("CompanyTwoBaseUrl")!;

        public string CompanyThreeBaseUrl => _configuration.GetValue<string>("CompanyThreeBaseUrl")!;
    }
}
