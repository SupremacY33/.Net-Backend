using Metronic_Backend.Models;

namespace Metronic_Backend.Repository
{
    public interface IUsersRepositories
    {

        Task<List<Users>> GetAllUsersAsync();

        Task<List<Users>> GetUsersAsyncById(int id);

        Task<List<Users>> DeleteUsersAsyncById(int id);

        Task<int> AddNewUserRecordAsync(string userName, string userEmail, string userPassword);

        Task<Users?> UpdateUserRecordAsyncById(int id, string userName, string userEmail, string userPassword);

    }
}
