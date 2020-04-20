using System.Collections.Generic;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEntityService<T> where T : IBaseEntity
    {
        /// <summary>
        ///     Получение списка сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        ///     Получение сотрудника по id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        ///     Сохранить изменения
        /// </summary>
        void Commit();

        /// <summary>
        ///     Добавить нового
        /// </summary>
        /// <param name="model"></param>
        void AddNew(T model);

        /// <summary>
        ///     Удалить
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}