using FC.CodeFlix.Catalog.UnitTests.Common;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Validations
{
    public class DomainValidationTestFixture
        : TestFixtureBase
    {
        public DomainValidationTestFixture()
           : base() { }

        public string GetTargetName()
        {
            return Faker.Commerce.ProductName().Replace(" ", "");
        }

        public string GetTargetValue()
        {
            return Faker.Commerce.ProductName();
        }
    }

    [CollectionDefinition(nameof(DomainValidationTestFixture))]
    public class DomainValidationTestFixtureCollection
        : ICollectionFixture<DomainValidationTestFixture>
    { }
}
