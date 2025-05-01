namespace Basics
{
    public class AsyncProcessor
    {
        public async Task<string> ProcessDataAsync()
        {
            await Task.Delay(100);

            // Simulate asynchronous work
            return "Processed";
        }
    }
}
