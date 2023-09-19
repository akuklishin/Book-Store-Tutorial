using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.ViewModels
{
	public class OrderVM
	{
		//order header
		public OrderHeader OrderHeader { get; set; }
		//order details
		public IEnumerable<OrderDetail> OrderDetail { get; set; }
	}
}
