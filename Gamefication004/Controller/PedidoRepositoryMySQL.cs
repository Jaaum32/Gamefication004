using Gamefication004.Generics;
using Gamification03.Interfaces;
using Gamification03.Model;
using MySql.Data.MySqlClient;

namespace Gamification03.Controller;

public class PedidoRepositoryMySql : Repository<Pedido>
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
}