﻿using ManageShop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Services.SalesInvoices.Contracts
{
    public interface SalesInvoiceRepository
    {
        Task Add(SalesInvoice salesInvoice);
    }
}
