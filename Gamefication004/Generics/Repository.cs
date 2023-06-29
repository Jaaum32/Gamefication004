using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Repository<T>
{
    private string connectionString;

    public Repository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<T> ObterTodos(string tableName)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {tableName}";

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T entity = PopulateObject(reader);
                    yield return entity;
                }
            }
        }
    }

    public T ObterPorId(string tableName, int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {tableName} WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            using (MySqlDataReader reader = command.ExecuteReader())
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

    public void Inserir(string tableName, T entity)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = BuildInsertQuery(tableName, entity);

            command.ExecuteNonQuery();
        }
    }

    public void Atualizar(string tableName, T entity)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = BuildUpdateQuery(tableName, entity);

            command.ExecuteNonQuery();
        }
    }

    public void Excluir(string tableName, int id)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {tableName} WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }

    private T PopulateObject(MySqlDataReader reader)
    {
        T entity = Activator.CreateInstance<T>();

        foreach (var property in typeof(T).GetProperties())
        {
            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
            {
                var value = reader[property.Name];
                property.SetValue(entity, value);
            }
        }

        return entity;
    }

    private string BuildInsertQuery(string tableName, T entity)
    {
        var propertyNames = new List<string>();
        var propertyValues = new List<string>();

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.Name != "Id")
            {
                var value = property.GetValue(entity);
                propertyNames.Add(property.Name);
                propertyValues.Add($"@{property.Name}");
            }
        }

        var query = $"INSERT INTO {tableName} ({string.Join(", ", propertyNames)}) " +
                    $"VALUES ({string.Join(", ", propertyValues)})";

        return query;
    }

    private string BuildUpdateQuery(string tableName, T entity)
    {
        var propertyAssignments = new List<string>();

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.Name != "Id")
            {
                var value = property.GetValue(entity);
                propertyAssignments.Add($"{property.Name} = @{property.Name}");
            }
        }

        var query = $"UPDATE {tableName} SET {string.Join(", ", propertyAssignments)} " +
                    $"WHERE Id = @Id";

        return query;
    }
}