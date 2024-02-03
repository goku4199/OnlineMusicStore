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

        //Funtionality Created and tested
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

        //Funtionality Created and tested
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

        //Funtionality Created and tested
        public void AddToCart(int userId, int songId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AddToCart", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@SongId", songId);
                    // Add other parameters as needed

                    command.ExecuteNonQuery();
                }
            }
        }

        //Funtionality Created and tested
        public Cart GetCartSongs(int userId)
        {
            
            int totalAmount = 0;
            List<Music> cartSongs = new List<Music>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetCartSongs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Music music = new Music
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Title = reader["Title"].ToString(),
                                Artist = reader["Artist"].ToString(),
                                Price = Convert.ToInt32(reader["Price"])
                                // Add other properties as needed
                            };

                            cartSongs.Add(music);
                            totalAmount = totalAmount + music.Price;
                        }
                    }
                }
            }
            Cart cart = new Cart
            {
                music = cartSongs,
                Price = totalAmount
            };

            return cart;
        }

    }
}
