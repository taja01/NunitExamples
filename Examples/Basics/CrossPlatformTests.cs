namespace Basics;

[TestFixture("windows")]
[TestFixture("linux")]
[TestFixture("macos")]
public class CrossPlatformTests
{
    private readonly string _os;

    public CrossPlatformTests(string os)
    {
        _os = os;
    }

    [Test]
    public void TestOperatingSystem()
    {
        Assert.That(_os, Is.Not.Null);
        TestContext.WriteLine($"Testing on {_os}");
    }
}