using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models.Interfaces;
using Logistic.ConsoleClient.Repositories.Interfaces;

namespace Logistic.ConsoleClient.Repositories
{
    public class InMemoryRepository<TEntity> : IRepositoryInMemory<TEntity>
        where TEntity : class, IEntity
    {
        private List<TEntity> entities;

        // Constructors
        public InMemoryRepository()
        {
            entities = new List<TEntity>();
        }

        public InMemoryRepository(List<TEntity> entities)
        {
            this.entities = entities;
            foreach (var entity in this.entities)
            {
                entity.Id = NextId();
            }
        }

        // Methods
        public void Create(TEntity entity)
        {

            //Mapper.CreateMap<TEntity, TEntity>();
            //Mapper.Map<TEntity, TEntity>(person2, person1);
            //throw new NotImplementedException();
            entity.Id = NextId();
            entities.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity Read(int id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> ReadAll()
        {
            return entities;
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }

        private int NextId()
        {
            int? maxId = entities.Any(e => e.Id != null) ? (int?)entities.Max(e => e.Id) : null;
            if (maxId != null)
            {
                return maxId.Value + 1;
            }

            return 1;

            // int? nextId = entities.Max(e => e.Id);
            // return entities.Max(x => x.Id ?? 0) + 1;
            // return 1;
        }
    }
}
