using BaseProject;

namespace EnvironmentExample
{
    public class Tests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine(base.EnvironmentName);

            var url = base.Configuration.GetSection("PetStoreOptions")["BaseAddress"];

            if (EnvironmentName.Equals("dev", StringComparison.OrdinalIgnoreCase))
            {
                Assert.That(url, Is.EqualTo("https://dev-petstore.com"));
            }
            else
            {
                Assert.That(url, Is.EqualTo("https://uat-petstore.com"));
            }
        }
    }
}
