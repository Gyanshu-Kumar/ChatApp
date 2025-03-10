using Npgsql;
using SimpleChatAppRabbitMQ.Models;

namespace SimpleChatAppRabbitMQ.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveMessage(string sender, string content)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("INSERT INTO Messages (Sender, Content, Timestamp) VALUES (@sender, @content, @timestamp)", connection);
            command.Parameters.AddWithValue("sender", sender);
            command.Parameters.AddWithValue("content", content);
            command.Parameters.AddWithValue("timestamp", DateTime.Now);

            command.ExecuteNonQuery();
        }

        public List<Message> GetMessages()
        {
            var messages = new List<Message>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT * FROM Messages ORDER BY Timestamp", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                messages.Add(new Message
                {
                    Id = reader.GetInt32(0),
                    Sender = reader.GetString(1),
                    Content = reader.GetString(2),
                    Timestamp = reader.GetDateTime(3)
                });
            }

            return messages;
        }
    }
}
