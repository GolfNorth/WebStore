using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public abstract class InMemoryEntityService<T> : IEntityService<T> where T : BaseViewModel
    {
        protected List<T> _entities;

        public IEnumerable<T> GetAll() => _entities;

        public T GetById(int id) => _entities.FirstOrDefault(e => e.Id == id);

        public void Commit() { }

        public void AddNew(T entity)
        {
            entity.Id = _entities.Max(e => e.Id) + 1;
            _entities.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);

            if (entity is null)
                return;

            _entities.Remove(entity);
        }
    }
}
