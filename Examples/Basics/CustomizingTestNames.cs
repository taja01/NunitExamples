namespace Basics;

[TestFixture]
public class CustomizingTestNames
{
    [TestCase(1, 2, 3, TestName = "Addition_When1and2_Returns3")]
    [TestCase(2, 5, 7, TestName = "Addition_When2and5_Returns7")]
    public void TestAddition(int a, int b, int expected)
    {
        Assert.That(a + b, Is.EqualTo(expected));
    }
}
