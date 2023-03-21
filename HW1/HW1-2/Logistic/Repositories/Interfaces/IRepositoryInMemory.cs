using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models.Interfaces;

namespace Logistic.ConsoleClient.Repositories.Interfaces
{
    public interface IRepositoryInMemory<TEntity>
        where TEntity : class, IEntity
    {
        void Create(TEntity entity);

        List<TEntity> ReadAll();

        TEntity Read(int id);

        void Update(int id);

        void Delete(int id);
    }
}
