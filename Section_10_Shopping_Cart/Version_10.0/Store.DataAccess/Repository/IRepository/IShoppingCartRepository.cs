﻿using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        //update shopping cart
        void Update(ShoppingCart obj);
    }
}
