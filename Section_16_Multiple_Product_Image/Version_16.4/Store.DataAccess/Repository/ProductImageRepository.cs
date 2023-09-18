using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using Store.Models;

namespace Store.DataAccess.Repository {
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository {
        private ApplicationDbContext _db;
        public ProductImageRepository(ApplicationDbContext db) : base(db){
            _db = db;    
        }

        public void Update(ProductImage obj) {
            _db.ProductImages.Update(obj);
        }
    }
}


