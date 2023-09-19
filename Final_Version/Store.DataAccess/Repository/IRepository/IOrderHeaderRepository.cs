using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        //update order header
        void Update(OrderHeader obj);
        //update order status
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        //update stripe payment id
        void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId);
	}
}
