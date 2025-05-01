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

        public static IEnumerable<TestCaseData> MultiplyTestCases
        {
            get
            {
                yield return new TestCaseData(2, 3, 6);
                yield return new TestCaseData(-1, 5, -5);
                yield return new TestCaseData(0, 10, 0);
            }
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

        [TestCase(1, 2, 3)]
        [TestCase(5, 5, 10)]
        [TestCase(-1, -1, -2)]
        public void Add_WithDifferentValues_ReturnsExpectedSum(int a, int b, int expected)
        {
            int result = _calculator.Add(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Divide_DenominatorIsZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
        }

        [Test, TestCaseSource(nameof(MultiplyTestCases))]
        public void Multiply_WithVariousInputs_ReturnsExpectedResult(int a, int b, int expected)
        {
            int result = _calculator.Multiply(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
