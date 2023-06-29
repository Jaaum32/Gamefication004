using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class Repository<T>
{
    private string connectionString;

    public Repository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<T> GetAll(string tableName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {tableName}";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T entity = PopulateObject(reader);
                    yield return entity;
                }
            }
        }
    }

    public T GetById(string tableName, int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {tableName} WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    T entity = PopulateObject(reader);
                    return entity;
                }
            }
        }

        return default(T);
    }

    public void Insert(string tableName, T entity)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = BuildInsertQuery(tableName, entity);

            command.ExecuteNonQuery();
        }
    }

    public void Update(string tableName, T entity)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = BuildUpdateQuery(tableName, entity);

            command.ExecuteNonQuery();
        }
    }

    public void Delete(string tableName, int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {tableName} WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}