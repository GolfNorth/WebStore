namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    ///     Интерфейс базовой сущности
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        ///     Идентификатор сущности
        /// </summary>
        int Id { get; set; }
    }
}