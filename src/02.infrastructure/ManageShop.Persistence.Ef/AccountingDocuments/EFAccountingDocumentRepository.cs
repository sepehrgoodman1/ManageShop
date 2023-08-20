using ManageShop.Entities.Entities;
using ManageShop.Services.AccountingDocuments.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageShop.Persistence.Ef.AccountingDocuments
{
    public class EFAccountingDocumentRepository : AccountingDocumentRepository
    {
        private readonly DbSet<AccountingDocument> _accountingDocuments;

        public EFAccountingDocumentRepository(EFDataContext context)
        {
            _accountingDocuments = context.AccountingDocuments;
        }

        public async Task Add(AccountingDocument document)
        {
            await _accountingDocuments.AddAsync(document);
        }
    }
}
