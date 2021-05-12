using NUnit.Framework;

public class SetCustomTest
{
    private TestCustom tc;

    [SetUp]
    public void Setup()
    {
        tc = new TestCustom();
        tc.Name = "Studious";
        tc.Age = 2021;

        SuperPlayerPrefs.DeleteAll();
        SuperPlayerPrefs.Set<TestCustom>("somekey", tc);
    }

    [Test]
    public void SetCustomClass()
    {
        Assert.AreEqual(typeof(TestCustom), SuperPlayerPrefs.Get<TestCustom>("somekey").GetType());
    }

    [Test]
    public void GetCustomClass()
    {
        Assert.AreEqual(2021, SuperPlayerPrefs.Get<TestCustom>("somekey").Age);
    }
}

public class TestCustom
{
    public string Name;
    public string Address;
    public int Age;
}
