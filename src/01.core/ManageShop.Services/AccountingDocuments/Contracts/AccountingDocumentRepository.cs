using ManageShop.Entities.Entities;

namespace ManageShop.Services.AccountingDocuments.Contracts
{
    public interface AccountingDocumentRepository
    {
        Task Add(AccountingDocument document);
    }
}
