using Bogus;

namespace FC.CodeFlix.Catalog.IntegrationTests.Common
{
    public abstract class TestFixtureBase
    {
        public Faker Faker { get; set; }

        protected TestFixtureBase()
            => Faker = new Faker("pt_BR");
    }
}
