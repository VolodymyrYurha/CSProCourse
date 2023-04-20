using Logistic.Models;
using Logistic.Models.Interfaces;

namespace Logistic.Core.Interfaces
{
    public interface IEntityLoadingService<TEntity>
        where TEntity : class, IEntity
    {
        void Create(TEntity entity);

        TEntity GetById(int id);

        List<TEntity> GetAll();

        void Delete(int id);

        void LoadCargo(Cargo cargo, int idEntity);

        void UnloadCargo(Guid guidCargo, int idEntity);
    }
}
