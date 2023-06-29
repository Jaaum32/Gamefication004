using MySql.Data.MySqlClient;

public class Repository<T>
{
    public MySqlConnection MySqlConnection =
        new MySqlConnection("Server=localhost;Port=3306;database=gamefication;uid=root;pwd=260405");

    public IEnumerable<T> ObterTodos(string tableName)
    {
        using (MySqlConnection)
        {
            MySqlConnection.Open();

            MySqlCommand command = MySqlConnection.CreateCommand();
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
        using (MySqlConnection)
        {
            MySqlConnection.Open();

            MySqlCommand command = MySqlConnection.CreateCommand();
            command.CommandText = $"SELECT * FROM {tableName} WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                T entity = PopulateObject(reader);
                return entity;
            }
        }

        return default(T);
    }

    public void Inserir(string tableName, T entity)
    {
        using (MySqlConnection)
        {
            MySqlConnection.Open();

            var propertyNames = new List<string>();
            var propertyValues = new List<string>();
            var parameters = new List<MySqlParameter>();

            foreach (var property in typeof(T).GetProperties())
            {
                if (property.Name != "Id")
                {
                    var value = property.GetValue(entity);
                    propertyNames.Add(property.Name);
                    propertyValues.Add($"@{property.Name}");
                    parameters.Add(new MySqlParameter($"@{property.Name}", value ?? DBNull.Value));
                }
            }

            var query = $"INSERT INTO {tableName} ({string.Join(", ", propertyNames)}) " +
                        $"VALUES ({string.Join(", ", propertyValues)})";


            MySqlCommand command = MySqlConnection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddRange(parameters.ToArray());

            command.ExecuteNonQuery();
        }
    }

    public void Atualizar(string tableName, T entity)
    {
        using (MySqlConnection)
        {
            MySqlConnection.Open();

            MySqlCommand command = MySqlConnection.CreateCommand();
            command.CommandText = BuildUpdateQuery(tableName, entity);

            command.ExecuteNonQuery();
        }
    }

    public void Excluir(string tableName, int id)
    {
        using (MySqlConnection)
        {
            MySqlConnection.Open();

            MySqlCommand command = MySqlConnection.CreateCommand();
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

    private string BuildUpdateQuery(string tableName, T entity)
    {
        var propertyAssignments = new List<string>();
        var parameters = new List<MySqlParameter>();

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.Name != "Id")
            {
                var value = property.GetValue(entity);
                propertyAssignments.Add($"{property.Name} = @{property.Name}");
                parameters.Add(new MySqlParameter($"@{property.Name}", value ?? DBNull.Value));
            }
        }

        var query = $"UPDATE {tableName} SET {string.Join(", ", propertyAssignments)} " +
                    $"WHERE Id = @Id";

        using (MySqlConnection)
        {
            MySqlConnection.Open();

            MySqlCommand command = MySqlConnection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddRange(parameters.ToArray());

            command.ExecuteNonQuery();
        }

        return query;
    }
}