namespace Homework2.test;
using Homework2.Classes;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Student test = new(1, "", "", "");
        Assert.True(test.Equals(test), "should equal itself");
    }
}
