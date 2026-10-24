using FlorMoon.Repository.Interfaces;

namespace FlorMoon.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Save();
    }
}
