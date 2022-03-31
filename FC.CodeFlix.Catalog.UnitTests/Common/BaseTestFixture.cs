using Bogus;

namespace FC.CodeFlix.Catalog.UnitTests.Common
{
    public abstract class BaseTestFixture
    {
        public Faker Faker { get; set; }

        protected BaseTestFixture() 
            => Faker = new Faker("pt_BR");
    }
}
