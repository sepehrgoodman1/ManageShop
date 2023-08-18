

using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration.Fixtures;
using ManageShop.Persistence.Ef;

namespace BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration
{


public class TenantIntegrationTest : EFDataContextDatabaseFixture
{
    protected EFDataContext SetupContext { get; }
    protected EFDataContext FirstDbContext { get; }
    protected EFDataContext SecondDbContext { get; }
    protected EFDataContext FirstReadContext { get; }
    protected EFDataContext SecondReadContext { get; }

    protected TenantIntegrationTest(
        string firstTenantId,
        string secondTenantId)
    {
        SetupContext = CreateDataContext(firstTenantId);
        FirstDbContext = CreateDataContext(firstTenantId);
        SecondDbContext = CreateDataContext(secondTenantId);
        FirstReadContext = CreateDataContext(firstTenantId);
        SecondReadContext = CreateDataContext(secondTenantId);
    }
}
}