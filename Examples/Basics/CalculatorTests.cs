namespace Basics
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_WhenCalled_ReturnsSumOfArguments()
        {
            // Arrange
            var calculator = new Calculator();

            int a = 5, b = 7;

            // Act
            int result = calculator.Add(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(12), "Addition did not return expected result.");
        }
    }
}
