using OnlineMusicStore.Models;
using System.Data.SqlClient;
using System.Data;

namespace OnlineMusicStore.Repository
{
    public class UserDataAccess
    {
        private readonly string connectionString;

        public UserDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void RegisterUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("RegisterUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Email", user.Email);

                    // ExecuteNonQuery since it's an INSERT operation
                    command.ExecuteNonQuery();
                }
            }
        }

        public User ValidateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ValidateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password); // Note: Hash your passwords in production!

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].ToString()
                            };
                        }
                    }
                }
            }

            return null; // User not found or invalid credentials
        }
    }
}
