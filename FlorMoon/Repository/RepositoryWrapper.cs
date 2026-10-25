using FlorMoon.Data;
using FlorMoon.Repository.Interfaces;

namespace FlorMoon.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _context;
        private IProductRepository _product;
        private ICategoryRepository _category;
        private IUserRepository _user;

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

        public IUserRepository UserRepository
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
