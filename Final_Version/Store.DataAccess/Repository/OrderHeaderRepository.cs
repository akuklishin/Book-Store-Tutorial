using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        //dependency injection
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        //update order status
        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
            //get order from db by id
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

            //if order exists
            if (orderFromDb != null)
			{
                //assign order status
                orderFromDb.OrderStatus = orderStatus;
                //if pamyment status is not empty
                if (!string.IsNullOrEmpty(paymentStatus))
				{
                    //assign payment status
                    orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}

        //update stripe payment id
        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
		{
            //get order from db by id
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            //if there is session id
            if (!string.IsNullOrEmpty(sessionId))
			{
                //assign session id
                orderFromDb.SessionId = sessionId;
			}
            //if there is payment intent id
            if (!string.IsNullOrEmpty(paymentIntentId))
			{
                //assign payment intent id
                orderFromDb.PaymentIntentId = paymentIntentId;
                //set payment date as datetime stamp
                orderFromDb.PaymentDate = DateTime.Now;
			}
		}
	}
}
