using Logistic.Models.Interfaces;

namespace Logistic.DAL.Interfaces
{
    public interface IInMemoryRepository<TEntity>
        where TEntity : class, IEntity
    {
        void Create(TEntity entity);

        List<TEntity> ReadAll();

        TEntity Read(int id);

        void Update(int id, TEntity updateTo);

        void Delete(int id);
    }
}
