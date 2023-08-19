﻿using ManageShop.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageShop.Services.NewProducts.Contracts
{
    public interface NewProductRepository
    {
        Task Add(List<NewProduct> newProducts);
    }
}
