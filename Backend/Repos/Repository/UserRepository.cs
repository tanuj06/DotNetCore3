using Backend.Contracts.DatabaseContext;
using Backend.Contracts.DataContracts;
using Backend.Repos.IRepository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repos.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext applicationDBContext)
        {
            _context = applicationDBContext;
        }

        public bool Authenticate(UserInput input)
        {
            throw new System.NotImplementedException();
        }

        public bool IsValidUser(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateUser(Users user)
        {
            var parameters = new[]
            {
            new NpgsqlParameter("firstName", user.FirstName),
            new NpgsqlParameter("lastName", user.LastName),
            new NpgsqlParameter("email", user.Email),
            new NpgsqlParameter("dateOfBirth", user.DateOfBirth)
        };

            await _context.Database.ExecuteSqlRawAsync("CALL CreateUser(@firstName, @lastName, @email, @dateOfBirth)", parameters);
        }

        public async Task UpdateUser(Users user)
        {
            var parameters = new[]
            {
            new NpgsqlParameter("userId", user.Id),
            new NpgsqlParameter("firstName", user.FirstName),
            new NpgsqlParameter("lastName", user.LastName),
            new NpgsqlParameter("email", user.Email),
            new NpgsqlParameter("dateOfBirth", user.DateOfBirth)
        };

            await _context.Database.ExecuteSqlRawAsync("CALL UpdateUser(@userId, @firstName, @lastName, @email, @dateOfBirth)", parameters);
        }



        public async Task<bool> DeleteUser(int id)
        {
            var param = new NpgsqlParameter("userId", id);
            int updated = await _context.Database.ExecuteSqlRawAsync("CALL DeleteUser(@userId)", param);
            return Convert.ToBoolean(updated);
        }

        public async Task<List<Users>> GetAllUsers()
        {
            var response = await _context.Users.FromSqlRaw("SELECT * FROM GetAllUsers()").ToListAsync();
            return response;
        }

        public async Task<Users> GetUserByID(int id)
        {
            var param = new NpgsqlParameter("userId", id);
            return await _context.Users
                .FromSqlRaw("SELECT * FROM GetUserById(@userId)", param)
                .FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserByName(string name)
        {
            var param = new NpgsqlParameter("firstName", name);
            return await _context.Users
                .FromSqlRaw("SELECT * FROM GetUserById(@userId)", param)
                .FirstOrDefaultAsync();
        }


    }
}
