using System.Reflection;
using Xunit.Sdk;

namespace BluePrint.TestTools.Infrastructure.DummyData
{
    public class DummyString : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return new[] { new object[] { "dummy_string" } };
        }
    }
}