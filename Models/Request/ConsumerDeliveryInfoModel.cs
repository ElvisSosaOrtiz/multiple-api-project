namespace Models.Request
{
    public class ConsumerDeliveryInfoModel
    {
        public required string SourceAddress { get; set; }

        public required string DestinationAddress { get; set; }

        public required List<string> CartonDimensions { get; set; }
    }
}
