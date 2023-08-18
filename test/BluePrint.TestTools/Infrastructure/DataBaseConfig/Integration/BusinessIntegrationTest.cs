
using BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration.Fixtures;
using ManageShop.Persistence.Ef;

namespace BluePrint.TestTools.Infrastructure.DataBaseConfig.Integration
{

public class BusinessIntegrationTest : EFDataContextDatabaseFixture
{
    protected EFDataContext SetupContext { get; }
    protected EFDataContext DbContext { get; }
    protected EFDataContext ReadContext { get; }
    protected string TenantId { get; } = "Tenant_Id";


    protected BusinessIntegrationTest(string? tenantId = null)
    {
        if (tenantId != null)
        {
            TenantId = tenantId;
        }

        SetupContext = CreateDataContext(TenantId);
        DbContext = CreateDataContext(TenantId);
        ReadContext = CreateDataContext(TenantId);
    }

    protected void Save<T>(T entity)
        where T : class
    {
        DbContext.Manipulate(_ => _.Add(entity));
    }

    protected void Save<T>(params T[] entities)
        where T : class
    {
        foreach (var entity in entities)
        {
            DbContext.Save(entity);
        }
    }
}
}