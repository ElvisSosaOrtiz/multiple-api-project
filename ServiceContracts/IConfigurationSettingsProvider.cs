namespace ServiceContracts
{
    public interface IConfigurationSettingsProvider
    {
        public string CompanyOneBaseUrl { get; }

        public string CompanyTwoBaseUrl { get; }

        public string CompanyThreeBaseUrl { get; }
    }
}
