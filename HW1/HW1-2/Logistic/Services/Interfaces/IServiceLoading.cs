using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Models.Interfaces;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient.Services.Interfaces
{
    public interface IServiceLoading<TEntity>
        where TEntity : class, IEntity
    {
        void Create(TEntity entity); // створити новий Entity(зберігається у InMemoryRepository)

        TEntity GetById(int id); // отримати Entity по айді

        List<TEntity> GetAll(); // отримати всі Entity

        void Delete(int id); // видалити Entity по айді

        void LoadCargo(Cargo cargo, int idEntity); // завантажити Cargo в Entity по Entity id

        void UnloadCargo(Guid guidCargo, int idEntity); // вивантажи Cargo по айді з Entity по Entity айді
    }
}
