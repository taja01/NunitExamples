using BaseProject;
using Microsoft.Extensions.DependencyInjection;
using PetStore;

namespace PetStoreTests
{
    [SetUpFixture]
    internal class TestFixture : BaseSetUpFixture
    {
        public override void RegisterTestFacilities(IServiceCollection serviceCollection)
        {
            base.RegisterTestFacilities(serviceCollection);

            serviceCollection.AddPetStoreClient(Configuration);
        }
    }
}
