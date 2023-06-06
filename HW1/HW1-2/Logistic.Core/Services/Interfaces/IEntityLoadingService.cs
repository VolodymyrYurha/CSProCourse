using Logistic.Models;
using Logistic.Models.Interfaces;

namespace Logistic.Core.Interfaces
{
    public interface IEntityLoadingService<TEntity>
        where TEntity : class, IEntity
    {
        TEntity Create(TEntity entity);

        TEntity GetById(int id);

        List<TEntity> GetAll();

        TEntity Delete(int id);

        TEntity Update(int id, TEntity entity);

        Cargo LoadCargo(Cargo cargo, int idEntity);

        Cargo UnloadCargo(Guid guidCargo, int idEntity);
    }
}
