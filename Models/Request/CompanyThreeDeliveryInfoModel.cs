namespace Models.Request
{
    public class CompanyThreeDeliveryInfoModel
    {
        public required string Source { get; set; }

        public required string Destination { get; set; }

        public required List<string> Packages { get; set; }
    }
}
