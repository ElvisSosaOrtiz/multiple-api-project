namespace Models.Response
{
    public class CompanyOneOfferModel
    {
        public string CompanyName { get; set; } = null!;

        public string ContactAddress { get; set; } = null!;

        public string WarehouseAddress { get; set; } = null!;

        public double OfferedTotal { get; set; }
    }
}
