namespace Routing
{
    public class CompanyThreeControllerRoutes
    {
        public const string Root = "api/company3";
        public const string OfferQuote = "offer-quote";

        public static string GetOfferQuote => $"{Root}/{OfferQuote}";
    }
}
