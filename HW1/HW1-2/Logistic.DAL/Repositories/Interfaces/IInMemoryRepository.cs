using Logistic.Models.Interfaces;

namespace Logistic.DAL.Interfaces
{
    public interface IInMemoryRepository<TEntity>
        where TEntity : class, IEntity
    {
        TEntity Create(TEntity entity);

        List<TEntity> ReadAll();

        TEntity Read(int id);

        TEntity Update(int id, TEntity updateTo);

        TEntity Delete(int id);
    }
}
