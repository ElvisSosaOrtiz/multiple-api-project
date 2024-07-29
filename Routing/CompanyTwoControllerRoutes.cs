namespace Routing
{
    public class CompanyTwoControllerRoutes
    {
        public const string Root = "api/company2";
        public const string OfferAmount = "offer-amount";

        public static string GetOfferAmount => $"{Root}/{OfferAmount}";
    }
}
