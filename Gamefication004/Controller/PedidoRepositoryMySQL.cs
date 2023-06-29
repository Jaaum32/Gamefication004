using Gamification03.Interfaces;
using Gamification03.Model;
using MySql.Data.MySqlClient;

namespace Gamification03.Controller;

public class PedidoRepositoryMySql : IPedidoRepository
{
    private MySqlConnection _mySqlConnection =
        new MySqlConnection("Persist Security Info=False;server=localhost;database=gamefication;uid=root;pwd=0406");

    private void InicializeDatabase()
    {
        try
        {
            //abre a conexao
            _mySqlConnection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void Adicionar(Pedido pedido)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText =
            "INSERT INTO PEDIDO(data, cliente, status_pedido) VALUES(@data, @nome, @status); SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@data", pedido.Data);
        cmd.Parameters.AddWithValue("@nome", pedido.Cliente);
        cmd.Parameters.AddWithValue("@status", pedido.Status);

        cmd.Connection = _mySqlConnection;

        pedido.Id = Convert.ToInt32(cmd.ExecuteScalar());
        cmd.ExecuteReader();
        _mySqlConnection.Close();
    }

    public void Atualizar(Pedido pedido)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "UPDATE Pedido SET data = @data, nome = @nome, status = @status WHERE id = @id";

        cmd.Parameters.AddWithValue("@data", pedido.Data);
        cmd.Parameters.AddWithValue("@nome", pedido.Cliente);
        cmd.Parameters.AddWithValue("@status", pedido.Status);
        cmd.Parameters.AddWithValue("@id", pedido.Id);

        cmd.Connection = _mySqlConnection;
        cmd.ExecuteReader();
        _mySqlConnection.Close();
    }
    
    public void AtualizarStatus(int id, string? status)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "UPDATE Pedido SET status_pedido1 = @status WHERE id = @id";

        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.Connection = _mySqlConnection;
        cmd.ExecuteReader();
        _mySqlConnection.Close();
    }

    public void Excluir(int id)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "DELETE FROM ItemPedido WHERE pedido_id = @pedido_id";

        cmd.Parameters.AddWithValue("@pedido_id", id);

        cmd.Connection = _mySqlConnection;
        cmd.ExecuteReader();
        _mySqlConnection.Close();
        
        InicializeDatabase();
        cmd.CommandText = "DELETE FROM pedido WHERE id = @id";

        cmd.Parameters.AddWithValue("@id", id);
        
        cmd.Connection = _mySqlConnection;
        cmd.ExecuteReader();
        _mySqlConnection.Close();
    }

    public Pedido ObterPorId(int id)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido WHERE id = @id";

        cmd.Connection = _mySqlConnection;
        cmd.Parameters.AddWithValue("@id", id);

        var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Pedido(id,
                Convert.ToString(reader["data"]),
                Convert.ToString(reader["cliente"]),
                Convert.ToString(reader["status_pedido"])
            );
        }
        
        _mySqlConnection.Close();
        return null!;
    }
    
    public IEnumerable<Pedido> ObterPorNome(string nome)
    {
        List<Pedido> pedidos = new List<Pedido>();
        
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido WHERE cliente LIKE @nome";

        cmd.Connection = _mySqlConnection;
        cmd.Parameters.AddWithValue("@nome", '%' + nome + '%');

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Pedido pedido = new Pedido(Convert.ToInt32(reader["id"]),
                Convert.ToString(reader["data"]),
                Convert.ToString(reader["cliente"]),
                Convert.ToString(reader["status_pedido"])
            );

            pedidos.Add(pedido);
        }
        
        _mySqlConnection.Close();
        return pedidos;
    }
    
    public IEnumerable<Pedido> ObterPorStatus(String status)
    {
        List<Pedido> pedidos = new List<Pedido>();
        
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido WHERE status_pedido = @status";

        cmd.Connection = _mySqlConnection;
        cmd.Parameters.AddWithValue("@status", status);

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Pedido pedido = new Pedido(Convert.ToInt32(reader["id"]),
                Convert.ToString(reader["data"]),
                Convert.ToString(reader["cliente"]),
                Convert.ToString(reader["status_pedido"])
            );

            pedidos.Add(pedido);
        }
        
        _mySqlConnection.Close();
        return pedidos;
    }
    
    public IEnumerable<Pedido> ObterPorData(String data)
    {
        List<Pedido> pedidos = new List<Pedido>();
        
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido WHERE data = @data";

        cmd.Connection = _mySqlConnection;
        cmd.Parameters.AddWithValue("@data", data);

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Pedido pedido = new Pedido(Convert.ToInt32(reader["id"]),
                Convert.ToString(reader["data"]),
                Convert.ToString(reader["cliente"]),
                Convert.ToString(reader["status_pedido"])
            );

            pedidos.Add(pedido);
        }
        
        _mySqlConnection.Close();
        return pedidos;
    }

    public IEnumerable<Pedido> ListarTodos()
    {
        List<Pedido> pedidos = new List<Pedido>();

        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido";

        cmd.Connection = _mySqlConnection;
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Pedido pedido = new Pedido(Convert.ToInt32(reader["id"]),
                Convert.ToString(reader["data"]),
                Convert.ToString(reader["cliente"]),
                Convert.ToString(reader["status_pedido"])
            );

            pedidos.Add(pedido);
        }
        
        _mySqlConnection.Close();
        return pedidos;
    }
}