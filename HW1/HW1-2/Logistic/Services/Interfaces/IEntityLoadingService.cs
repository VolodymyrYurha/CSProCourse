using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.Models;
using Logistic.Models.Interfaces;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient.Services.Interfaces
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
