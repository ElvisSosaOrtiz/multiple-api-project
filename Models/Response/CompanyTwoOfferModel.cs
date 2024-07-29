namespace Models.Response
{
    public class CompanyTwoOfferModel
    {
        public string CompanyName { get; set; } = null!;

        public string Consignee { get; set; } = null!;

        public string Consignor { get; set; } = null!;

        public double OfferedAmount { get; set; }
    }
}
