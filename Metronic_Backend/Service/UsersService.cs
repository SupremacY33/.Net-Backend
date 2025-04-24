using Metronic_Backend.Models;
using Metronic_Backend.Repository;

namespace Metronic_Backend.Service

{
    public class UsersService : IUsersService
    {

        private readonly IUsersRepositories _usersRepository;

        public UsersService(IUsersRepositories usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            return await _usersRepository.GetAllUsersAsync();
        }

        public async Task<List<Users>> GetUsersAsyncById(int Id)
        {
            return await _usersRepository.GetUsersAsyncById(Id);
        }

        public async Task<List<Users>> DeleteUsersAsyncById(int Id)
        {
            return await _usersRepository.DeleteUsersAsyncById(Id);
        }

        public async Task<bool> AddNewUserRecordAsync(string userName, string userEmail, string userPassword)
        {
            int result = await _usersRepository.AddNewUserRecordAsync(userName, userEmail, userPassword);
            return result > 0;
        }

        public async Task<Users?> UpdateUserRecordAsyncById(int id, string userName, string userEmail, string userPassword)
        {
            return await _usersRepository.UpdateUserRecordAsyncById(id, userName, userEmail, userPassword);
        }

    }
}
