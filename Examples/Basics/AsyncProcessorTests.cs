namespace Basics
{
    [TestFixture]
    public class AsyncProcessorTests
    {
        [Test]
        public async Task ProcessDataAsync_ReturnsProcessed()
        {
            var processor = new AsyncProcessor();
            string result = await processor.ProcessDataAsync();
            Assert.That(result, Is.EqualTo("Processed"));
        }
    }
}
