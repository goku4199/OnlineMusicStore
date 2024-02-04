﻿using OnlineMusicStore.Models;
using System.Data;
using System.Data.SqlClient;

namespace OnlineMusicStore.Repository
{
    public class MusicDataAccess
    {
        private readonly string connectionString;

        public MusicDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Funtionality Created and tested
        public List<Music> GetAllMusic()
        {
            List<Music> musicList = new List<Music>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetMusicsAll", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Music music = new Music
                            {
                                id = Convert.ToInt32(reader["Id"]),
                                title = reader["Title"].ToString(),
                                artist = reader["Artist"].ToString(),
                                price = Convert.ToInt32(reader["Price"])
                                // Add other properties as needed
                            };

                            musicList.Add(music);
                        }
                    }
                }
            }

            return musicList;

        }

        //Funtionality Created and tested
        public Music GetMusicById(int id)
        {
            Music music = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetMusicById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the stored procedure
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if a record was returned
                        if (reader.Read())
                        {
                            music = new Music
                            {
                                id = Convert.ToInt32(reader["Id"]),
                                title = reader["Title"].ToString(),
                                artist = reader["Artist"].ToString(),
                                price = Convert.ToInt32(reader["Price"])
                                // Add other properties as needed
                            };
                        }
                    }
                }
            }

            return music;
        }

        //Funtionality Created and tested
        public void CreateMusic(Music music)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CreateMusic", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@Title", music.title);
                    command.Parameters.AddWithValue("@Artist", music.artist);
                    command.Parameters.AddWithValue("@Price", music.price);

                    // ExecuteNonQuery for INSERT operations
                    command.ExecuteNonQuery();
                }
            }
        }

        //Funtionality Created and tested
        public void UpdateMusic(Music music)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateMusic", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@Id", music.id);
                    command.Parameters.AddWithValue("@Title", music.title);
                    command.Parameters.AddWithValue("@Artist", music.artist);
                    command.Parameters.AddWithValue("@Price", music.price);

                    // ExecuteNonQuery for UPDATE operations
                    command.ExecuteNonQuery();
                }
            }
        }

        //Funtionality Created and tested
        public void DeleteMusic(int musicId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteMusic", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter
                    command.Parameters.AddWithValue("@Id", musicId);

                    // ExecuteNonQuery for DELETE operations
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
