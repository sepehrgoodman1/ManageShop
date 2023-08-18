using System.Reflection;
using Xunit.Sdk;

namespace BluePrint.TestTools.Infrastructure.DummyData
{


public class DummyThreeString : DataAttribute
    {
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        return new[]
        {
            new object[]
            {
                "dummy_string_first",
                "dummy_string_second",
                "dummy_string_three"
            }
        };
    }
}
}