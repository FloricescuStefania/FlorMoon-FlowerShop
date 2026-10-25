using FlorMoon.Repository.Interfaces;

namespace FlorMoon.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        void Save();
    }
}
