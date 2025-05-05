namespace Basics;

[TestFixture]
public class SetupTeardownExample
{
    private List<string>? _data;

    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        TestContext.Out.WriteLine("Setting up the Test Suite...");
        _data = new List<string>();
    }

    [SetUp]
    public void TestSetUp()
    {
        TestContext.Out.WriteLine("Setting up before each test...");
        _data.Clear();
    }

    [Test]
    public void TestExample1()
    {
        _data.Add("Example Data");
        Assert.That(_data.Count, Is.EqualTo(1));
    }

    [Test]
    public void TestExample2()
    {
        Assert.That(_data, Is.Empty);
    }

    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        TestContext.Out.WriteLine("Cleaning up the Test Suite...");
        _data = null;
    }
}