namespace Models.Response
{
    public class ConsumerOfferModel
    {
        public string CompanyName { get; set; } = null!;

        public string SourceAddress { get; set; } = null!;

        public string DestinationAddress { get; set; } = null!;

        public double Offer { get; set; }
    }
}
