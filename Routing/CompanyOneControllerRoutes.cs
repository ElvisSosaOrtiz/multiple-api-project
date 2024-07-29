namespace Routing
{
    public class CompanyOneControllerRoutes
    {
        public const string Root = "api/company1";
        public const string OfferTotal = "offer-total";

        public static string GetOfferTotal => $"{Root}/{OfferTotal}";
    }
}
