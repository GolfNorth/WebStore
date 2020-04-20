using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities.Base.Interfaces;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public abstract class InMemoryEntityService<T> : IEntityService<T> where T : IBaseEntity
    {
        protected List<T> Entities;

        public IEnumerable<T> GetAll()
        {
            return Entities;
        }

        public T GetById(int id)
        {
            return Entities.FirstOrDefault(e => e.Id == id);
        }

        public void Commit()
        {
        }

        public void AddNew(T entity)
        {
            entity.Id = Entities.Max(e => e.Id) + 1;
            Entities.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);

            if (entity is null)
                return;

            Entities.Remove(entity);
        }
    }
}