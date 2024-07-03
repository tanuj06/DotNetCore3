using Backend.Contracts.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repos.IRepository
{
    public interface IUserRepository
    {
        public bool IsValidUser(string email);

        public bool Authenticate(UserInput input);

        Task<List<Users>> GetAllUsers();

        Task<Users> GetUserByID(int id);

        Task<Users> GetUserByName(string name);

        Task CreateUser(Users user);

        Task UpdateUser(Users user);

        Task<bool> DeleteUser(int id);
    }
}
