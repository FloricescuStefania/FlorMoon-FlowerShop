using FlorMoon.Data;
using FlorMoon.Repository.Interfaces;

namespace FlorMoon.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _context;
        private IProductRepository _product;
        private ICategoryRepository _category; 

        public RepositoryWrapper(AppDbContext context)
        {
            _context = context;
        }
        public IProductRepository ProductRepository
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_context);
                }
                return _product;
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }
                return _category;
            }
        }
       
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
