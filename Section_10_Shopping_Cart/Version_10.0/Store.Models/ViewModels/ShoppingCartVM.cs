using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.ViewModels
{
    public class ShoppingCartVM
    {
        //shopping cart list
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        //Order header
        public OrderHeader OrderHeader { get; set; }
    }
}
