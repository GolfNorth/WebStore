using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Models
{
    public class EmployeeViewModel : IBaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }
}