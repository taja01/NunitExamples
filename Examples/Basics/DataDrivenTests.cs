namespace Basics;

[TestFixture]
public class DataDrivenTests
{
    static object[] TestCases =
    {
        new object[] { 1, 2, 3 },
        new object[] { -1, -1, -2 },
        new object[] { 0, 0, 0 }
    };

    [TestCaseSource(nameof(TestCases))]
    public void AddTest(int a, int b, int expected)
    {
        Assert.That(a + b, Is.EqualTo(expected));
    }
}