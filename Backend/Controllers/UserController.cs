using Backend.Contracts;
using Backend.Contracts.DataContracts;
using Backend.Repos.IRepository;
using Backend.Repos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<Users>>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var response = new ApiResponse<List<Users>>
            {
                ResponseCode = 200,
                ResponseMessage = "Success",
                Data = users
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Users>>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByID(id);
            if (user == null)
            {
                return NotFound(new ApiResponse<Users>
                {
                    ResponseCode = 404,
                    ResponseMessage = "User not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse<Users>
            {
                ResponseCode = 200,
                ResponseMessage = "Success",
                Data = user
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Users>>> GetUserByName(string name)
        {
            var user = await _userRepository.GetUserByName(name);
            if (user == null)
            {
                return NotFound(new ApiResponse<Users>
                {
                    ResponseCode = 404,
                    ResponseMessage = "User not found",
                    Data = null
                });
            }
            return Ok(new ApiResponse<Users>
            {
                ResponseCode = 200,
                ResponseMessage = "Success",
                Data = user
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> CreateUser([FromBody] Users user)
        {
            await _userRepository.CreateUser(user);
            return Ok(new ApiResponse<Users>
            {
                ResponseCode = 201,
                ResponseMessage = "User created successfully",
                Data = user
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Users>>> UpdateUser(int id, [FromBody] Users user)
        {
            var existingUser = await _userRepository.GetUserByID(id);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<Users>
                {
                    ResponseCode = 404,
                    ResponseMessage = "User not found",
                    Data = null
                });
            }
            user.Id = id;
            await _userRepository.UpdateUser(user);
            return Ok(new ApiResponse<Users>
            {
                ResponseCode = 200,
                ResponseMessage = "User updated successfully",
                Data = user
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteUser(int id)
        {
            var existingUser = await _userRepository.GetUserByID(id);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<bool>
                {
                    ResponseCode = 404,
                    ResponseMessage = "User not found",
                    Data = false
                });
            }
            bool deleted = await _userRepository.DeleteUser(id);
            return Ok(new ApiResponse<bool>
            {
                ResponseCode = 200,
                ResponseMessage = "User deleted successfully",
                Data = deleted
            });
        }
    }


}



