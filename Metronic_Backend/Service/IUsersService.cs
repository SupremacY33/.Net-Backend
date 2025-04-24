using Metronic_Backend.Models;

namespace Metronic_Backend.Service
{
    public interface IUsersService
    {

        Task<List<Users>> GetUsersAsync();

        Task<List<Users>> GetUsersAsyncById(int id);

        Task<List<Users>> DeleteUsersAsyncById(int id);

        Task<bool> AddNewUserRecordAsync(string userName, string userEmail, string userPassword);

        Task<Users?> UpdateUserRecordAsyncById(int id, string userName, string userEmail, string userPassword);

    }
}
