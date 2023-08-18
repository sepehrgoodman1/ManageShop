using System.Reflection;
using Xunit.Sdk;

namespace BluePrint.TestTools.Infrastructure.DummyData
{
    public class DummyFileAndGuid : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var fileId = Guid.NewGuid();
            var fileExtension = "dummy";
            var creatorId = Guid.NewGuid();
            return new[] { new object[] { fileId, fileExtension, creatorId } };
        }
    }
}