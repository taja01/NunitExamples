namespace Basics
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Init()
        {
            // Runs before each test method
            _calculator = new Calculator();
        }

        [Test]
        public void Add_WithPositiveNumbers_ReturnsCorrectSum()
        {
            int result = _calculator.Add(3, 4);
            Assert.That(result, Is.EqualTo(7));
        }

        [Test]
        public void Add_WithNegativeNumbers_ReturnsCorrectSum()
        {
            int result = _calculator.Add(-2, -3);
            Assert.That(result, Is.EqualTo(-5));
        }
    }
}
