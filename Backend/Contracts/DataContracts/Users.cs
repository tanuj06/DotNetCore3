using System;

namespace Backend.Contracts.DataContracts
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
    }
}
