using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Repositories
{
    internal class XmlRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public void Create(List<TEntity> entities)
        {

        }

        public List<TEntity> Read(string filename)
        {
            return null;
        }
    }
}
