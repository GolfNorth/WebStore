﻿namespace WebStore.Models
{
    public class EmployeeViewModel : BaseViewModel
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }
}