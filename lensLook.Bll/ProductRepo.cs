using lensLook.Dal;
using lensLook.Dal.Context;
using lensLook.Dal.models;

namespace lensLook.Bll
{
    public class ProductRepo : IProductRepo
    {
        private readonly LensLookDbContext _context;

        public ProductRepo(LensLookDbContext context)
        {
            _context = context;
        }

        public bool Create(Product Model)
        {
            try
            {
                _context.Products.Add(Model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
                
            }
        }

        public bool Delete(int Model)
        {
            try
            {
                var Product = GetProductById(Model);
                _context.Remove(Product);
                _context.SaveChanges();
                return true;
            }

            catch (Exception)
            {
                return false;
                throw;
            }
        

        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }
    }
}
