namespace Models.Request
{
    using Newtonsoft.Json;

    public class CompanyTwoDeliveryInfoModel
    {
        [JsonProperty("consignee")]
        public required string Consignee { get; set; }

        [JsonProperty("consignor")]
        public required string Consignor { get; set; }

        [JsonProperty("cartons")]
        public required List<string> Cartons { get; set; }
    }
}
