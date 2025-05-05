namespace Basics;

public class TestCategories
{
    [Test]
    [Category("Integration")]
    public void IntegrationTest()
    {
        Assert.Pass("This is an integration test.");
    }

    [Test]
    [Category("Unit")]
    public void UnitTest()
    {
        Assert.Pass("This is a unit test.");
    }
}
//--where "cat==Integration"