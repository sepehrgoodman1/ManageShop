using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Entities.Entities
{
    public class AccountingDocument
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SalesInvoiceId { get; set; }

        public SalesInvoice SalesInvoice { get; set; }
    }
}
