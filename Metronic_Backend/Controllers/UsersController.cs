using Metronic_Backend.Models;
using Metronic_Backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Metronic_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersServices;

        public UsersController(IUsersService usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAllUsersRecords()
        {

            var completeUsersRecord = await _usersServices.GetUsersAsync();
            return Ok(completeUsersRecord);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Users>>> GetUsersRecordsById(int id)
        {

            var usersRecordById = await _usersServices.GetUsersAsyncById(id);
            if (usersRecordById == null)
            {
                return BadRequest(
                        $"User With Id: {id} Was Not Found"
                    );
            }

            return Ok(usersRecordById);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Users>>> DeleteUsersAsyncById(int id)
        {

            var checkRecordWithId = await _usersServices.DeleteUsersAsyncById(id);
            if (checkRecordWithId == null)
            {
                return NotFound(
                        $"User With Id: {id} Was Already Deleted"
                    );
            }

            return Ok(checkRecordWithId);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddNewUserRecordAsync([FromBody] Users users)
        {
            bool userIsCreated = await _usersServices.AddNewUserRecordAsync(users.UserName, users.UserEmail, users.UserPassword);

            if (userIsCreated == false) { 
                return BadRequest(
                        "User Cannot Be Created"
                    );
            }

            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> UpdateUserRecordAsyncById(int id,[FromBody] Users updateUsers)
        {
            if (updateUsers == null)
            {
                return BadRequest("Input Users Data/Record");
            }

            var updatedUserResult = await _usersServices.UpdateUserRecordAsyncById(
                    id, updateUsers.UserName, updateUsers.UserEmail, updateUsers.UserPassword
            );

            if (updatedUserResult == null) {
                return NotFound($"User With Id: {id} Not Found Or Failed To Update");
            }

            return Ok(updatedUserResult);

        }
    }
}
