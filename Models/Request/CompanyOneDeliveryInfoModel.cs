namespace Models.Request
{
    using Newtonsoft.Json;

    public class CompanyOneDeliveryInfoModel
    {
        [JsonProperty("contact_address")]
        public required string ContactAddress { get; set; }

        [JsonProperty("warehouse_address")]
        public required string WarehouseAddress { get; set; }

        [JsonProperty("package_dimensions")]
        public required List<string> PackageDimensions { get; set; }
    }
}
