namespace Tests
{
    using Microsoft.Extensions.Logging;
    using Models.Request;
    using Moq;
    using Services;
    using Tests.Builders;

    [TestFixture]
    public class CompanyAPIServiceTests
    {
        public class GetOfferedTotalTests
        {
            [Test]
            public void WhenPackageDimensionsCountIsZero_ShouldThrowNullReferenceException()
            {
                var expectedDeliveryInfo = new CompanyOneDeliveryInfoModel
                {
                    ContactAddress = "contact-address",
                    WarehouseAddress = "warehouse-address",
                    PackageDimensions = []
                };
                var instance = new CompanyAPIServiceBuilder().Build();

                Assert.Throws<NullReferenceException>(() => instance.GetOfferedTotal(expectedDeliveryInfo), "Should contain package dimensions.");
            }

            [TestCase("abc")]
            [TestCase("abcde")]
            [TestCase("abcdefgh")]
            [TestCase("abcdefghijkl")]
            public void ShouldReturnCompanyOneOfferModel(string packageDimensions)
            {
                var stringList = packageDimensions.ToCharArray().Select(s => s.ToString()).ToList();
                var expectedDeliveryInfo = new CompanyOneDeliveryInfoModel
                {
                    ContactAddress = "contact-address",
                    WarehouseAddress = "warehouse-address",
                    PackageDimensions = stringList
                };
                var instance = new CompanyAPIServiceBuilder().Build();

                var actual = instance.GetOfferedTotal(expectedDeliveryInfo);

                Assert.That(actual.CompanyName, Is.EqualTo("Company1"));
            }
        }

        public class GetOfferedAmountTests
        {
            [Test]
            public void WhenCartonsCountIsZero_ShouldThrowNullReferenceException()
            {
                var expectedDeliveryInfo = new CompanyTwoDeliveryInfoModel
                {
                    Consignee = "consignee",
                    Consignor = "consignor",
                    Cartons = []
                };
                var instance = new CompanyAPIServiceBuilder().Build();

                Assert.Throws<NullReferenceException>(() => instance.GetOfferedAmount(expectedDeliveryInfo), "Should contain cartons.");
            }

            [TestCase("abc")]
            [TestCase("abcde")]
            [TestCase("abcdefgh")]
            [TestCase("abcdefghijkl")]
            public void ShouldReturnCompanyTwoOfferModel(string cartons)
            {
                var stringList = cartons.ToCharArray().Select(s => s.ToString()).ToList();
                var expectedDeliveryInfo = new CompanyTwoDeliveryInfoModel
                {
                    Consignee = "consignee",
                    Consignor = "consignor",
                    Cartons = stringList
                };
                var instance = new CompanyAPIServiceBuilder().Build();

                var actual = instance.GetOfferedAmount(expectedDeliveryInfo);

                Assert.That(actual.CompanyName, Is.EqualTo("Company2"));
            }
        }

        public class GetOfferedQuoteTests
        {
            [Test]
            public void WhenPackagesCountIsZero_ShouldThrowNullReferenceException()
            {
                var expectedDeliveryInfo = new CompanyThreeDeliveryInfoModel
                {
                    Source = "source",
                    Destination = "destination",
                    Packages = []
                };
                var instance = new CompanyAPIServiceBuilder().Build();

                Assert.Throws<NullReferenceException>(() => instance.GetOfferedQuote(expectedDeliveryInfo), "Should contain packages.");
            }

            [TestCase("abc")]
            [TestCase("abcde")]
            [TestCase("abcdefgh")]
            [TestCase("abcdefghijkl")]
            public void ShouldReturnCompanyThreeOfferModel(string packages)
            {
                var stringList = packages.ToCharArray().Select(s => s.ToString()).ToList();
                var expectedDeliveryInfo = new CompanyThreeDeliveryInfoModel
                {
                    Source = "source",
                    Destination = "destination",
                    Packages = stringList
                };
                var instance = new CompanyAPIServiceBuilder().Build();

                var actual = instance.GetOfferedQuote(expectedDeliveryInfo);

                Assert.That(actual.CompanyName, Is.EqualTo("Company3"));
            }
        }
    }
}
