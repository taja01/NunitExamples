using BaseProject;
using Microsoft.Extensions.DependencyInjection;
using PetStore;
using SimpleWait.Core;

namespace PetStoreTests
{
    [TestFixture]
    internal class PetTests : BaseTest
    {
        private IPetStoreClient _client;
        private Pet pet;

        [OneTimeSetUp]
        public void Setup()
        {
            _client = base.ServiceProvider.GetRequiredService<IPetStoreClient>();
            pet = new Pet
            {
                Category = new Category
                {
                    Id = 1,
                    Name = "Dog"
                },
                Name = "Nero",
                Status = PetStatus.Pending,
                Id = new Random().NextInt64(0, long.MaxValue),
            };
        }



        [Test]
        [Order(1)]
        public void GetTest()
        {
            Assert.ThrowsAsync<ApiException>(async () => await _client.GetPetByIdAsync(pet.Id.Value).ConfigureAwait(false));
        }

        [Test]
        [Order(2)]
        public async Task AddNewPetTest()
        {
            await _client.AddPetAsync(pet).ConfigureAwait(false);
        }

        [Test]
        [Order(3)]
        public async Task GetTest_AfterAdded()
        {
            var petResponse = await AsyncWait.Initialize()
                .Timeout(TimeSpan.FromSeconds(10))
                .IgnoreExceptionTypes(typeof(PetStore.ApiException))
                .UntilAsync(async () =>
                {
                    var r = await _client.GetPetByIdAsync(pet.Id.Value).ConfigureAwait(false);

                    return r ?? null;
                });


            Assert.That(petResponse, Is.Not.Null);
        }

        [Test]
        [Order(4)]
        public async Task UpdatePetTest()
        {
            pet.Name = "Bernie";

            await _client.UpdatePetAsync(pet).ConfigureAwait(false);
        }

        [Test]
        [Order(5)]
        public async Task GetTest_AfterUpdate()
        {
            var petResponse = await AsyncWait.Initialize()
                   .Timeout(TimeSpan.FromSeconds(10))
                   .UntilAsync(async () =>
                   {
                       var r = await _client.GetPetByIdAsync(pet.Id.Value).ConfigureAwait(false);

                       if (r != null && r.Name == "Bernie") //Petstore instable? bug?
                       {
                           return r;
                       }
                       else return null;
                   });

            Assert.That(petResponse, Is.Not.Null);
            Assert.That(petResponse.Name, Is.EqualTo(pet.Name));
        }

        [Test]
        [Order(6)]
        public async Task Delete()
        {
            await _client.DeletePetAsync(pet.Name, pet.Id.Value).ConfigureAwait(false);
        }
    }
}