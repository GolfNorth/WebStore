namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    ///     Интерфейс упорядоченной сущности
    /// </summary>
    public interface IOrderedEntity
    {
        /// <summary>
        ///     Порядок сущности
        /// </summary>
        int Order { get; set; }
    }
}