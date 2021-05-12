using NUnit.Framework;

public class SetStringTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void SetString()
    {
        SuperPlayerPrefs.DeleteAll();
        SuperPlayerPrefs.Set<string>("somekey", "Hello and welcome");
        Assert.AreEqual("Hello and welcome", SuperPlayerPrefs.Get<string>("somekey"));
    }

}
