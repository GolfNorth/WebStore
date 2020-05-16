namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>
    ///     Интерфейс именованной сущности
    /// </summary>
    public interface INamedEntity : IBaseEntity
    {
        /// <summary>
        ///     Имя сущности
        /// </summary>
        string Name { get; set; }
    }
}