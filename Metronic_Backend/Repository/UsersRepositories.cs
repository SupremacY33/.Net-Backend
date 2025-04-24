using Metronic_Backend.Models;
using Microsoft.Data.SqlClient;

namespace Metronic_Backend.Repository
{
    public class UsersRepositories : IUsersRepositories
    {

        private readonly IConfiguration _configuration;

        public UsersRepositories(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {

            var usersRecords = new List<Users>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection =  new SqlConnection(connectionString))
            {

                var sqlCommand = new SqlCommand("Select * from dbo.users", connection);
                await connection.OpenAsync();
                var sqlCommandReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlCommandReader.ReadAsync())
                {
                    usersRecords.Add(new Users
                    {
                        Id = (int)sqlCommandReader["id"],
                        UserName = sqlCommandReader["userName"].ToString(),
                        UserEmail = sqlCommandReader["userEmail"].ToString(),
                        UserPassword = sqlCommandReader["userPassword"].ToString()
                    });
                }

            }

            return usersRecords;

        }

        public async Task<List<Users>> GetUsersAsyncById(int id)
        {
            var userRecordById = new List<Users>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var conn = new SqlConnection(connectionString))
            {
                var sqlCommandForId = new SqlCommand($"Select * from dbo.users WHERE Id={id}", conn);
                await conn.OpenAsync();
                var sqlCommandReaderForId = await sqlCommandForId.ExecuteReaderAsync();

                while (await sqlCommandReaderForId.ReadAsync())
                {
                    userRecordById.Add(new Users
                    {
                        Id = (int)sqlCommandReaderForId["id"],
                        UserName = sqlCommandReaderForId["userName"].ToString(),
                        UserEmail = sqlCommandReaderForId["userEmail"].ToString(),
                        UserPassword = sqlCommandReaderForId["userPassword"].ToString()
                    });
                }
            }

            return userRecordById;
        }

        public async Task<List<Users>> DeleteUsersAsyncById(int id)
        {
            var findUserById = new List<Users>();
            var connectionStrings = _configuration.GetConnectionString("DefaultConnection");

            using (var connecting = new SqlConnection(connectionStrings))
            {
                var sqlCommandForId = new SqlCommand($"Delete from dbo.users WHERE Id={id}", connecting);
                await connecting.OpenAsync();
                var sqlCmdReader = await sqlCommandForId.ExecuteReaderAsync();

                while (await sqlCmdReader.ReadAsync())
                {
                    findUserById.Remove(new Users {
                        Id = (int)sqlCmdReader["id"],
                        UserName = sqlCmdReader["userName"].ToString(),
                        UserEmail = sqlCmdReader["userEmail"].ToString(),
                        UserPassword = sqlCmdReader["userPassword"].ToString()
                    });
                }
            }

            return findUserById;
        }

        public async Task<int> AddNewUserRecordAsync(string userName, string userEmail, string userPassword)
        {

            var connect = _configuration.GetConnectionString("DefaultConnection");

            using (var connects = new SqlConnection(connect))
            {
                string sqlQuery = $"Insert into dbo.users (userName, userEmail, userPassword) VALUES (@userName, @userEmail, @userPassword)";

                using (SqlCommand command = new SqlCommand(sqlQuery, connects))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@userEmail", userEmail);
                    command.Parameters.AddWithValue("@userPassword", userPassword);

                    await connects.OpenAsync();
                    return await command.ExecuteNonQueryAsync();
                }
            }

        }

        public async Task<Users?> UpdateUserRecordAsyncById(int id, string userName, string userEmail, string userPassword)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                string updateQuery = @"UPDATE dbo.Users
                               SET userName = @userName,
                                   userEmail = @userEmail,
                                   userPassword = @userPassword
                               OUTPUT INSERTED.*
                               WHERE Id = @id";

                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@userEmail", userEmail);
                    command.Parameters.AddWithValue("@userPassword", userPassword);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Users
                            {
                                Id = (int)reader["Id"],
                                UserName = (string)reader["userName"],
                                UserEmail = (string)reader["userEmail"],
                                UserPassword = (string)reader["userPassword"]
                            };
                        }
                    }
                }
            }

            return null;
        }

    }
}
