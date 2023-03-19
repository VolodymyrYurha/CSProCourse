using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Repositories
{
    internal interface IRepository<TEntity>
        where TEntity : class
    {
        void Create(List<TEntity> entities);

        List<TEntity> Read(string filename);
    }
}
