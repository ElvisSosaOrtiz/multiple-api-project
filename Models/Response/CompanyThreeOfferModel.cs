namespace Models.Response
{
    public class CompanyThreeOfferModel
    {
        public string CompanyName { get; set; } = null!;

        public string Source { get; set; } = null!;

        public string Destination { get; set; } = null!;

        public double OfferedQuote { get; set; }
    }
}
