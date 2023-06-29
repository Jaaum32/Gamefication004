using Gamification03.Model;
using MySql.Data.MySqlClient;

namespace Gamification03.Controller;

public class PedidoRepositoryMySql : Repository<Pedido>
{
    
    private void InicializeDatabase()
    {
        try
        {
            //abre a conexao
            MySqlConnection.Open();
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

        cmd.CommandText = "UPDATE Pedido SET status = @status WHERE id = @id";

        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.Connection = MySqlConnection;
        cmd.ExecuteReader();
        MySqlConnection.Close();
    }
    public IEnumerable<Pedido> ObterPorNome(string nome)
    {
        List<Pedido> pedidos = new List<Pedido>();
        
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido WHERE cliente LIKE @nome";

        cmd.Connection = MySqlConnection;
        cmd.Parameters.AddWithValue("@nome", '%' + nome + '%');

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Pedido pedido = new Pedido(Convert.ToInt32(reader["id"]),
                Convert.ToDateTime(reader["data"]),
                Convert.ToString(reader["cliente"]),
                Convert.ToString(reader["status"])
            );

            pedidos.Add(pedido);
        }
        
        MySqlConnection.Close();
        return pedidos;
    }
    
    public IEnumerable<Pedido> ObterPorStatus(String status)
    {
        List<Pedido> pedidos = new List<Pedido>();
        
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido WHERE status = @status";

        cmd.Connection = MySqlConnection;
        cmd.Parameters.AddWithValue("@status", status);

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Pedido pedido = new Pedido(Convert.ToInt32(reader["id"]),
                Convert.ToDateTime(reader["data"]),
                Convert.ToString(reader["cliente"]),
                Convert.ToString(reader["status"])
            );

            pedidos.Add(pedido);
        }
        
        MySqlConnection.Close();
        return pedidos;
    }
    
    public IEnumerable<Pedido> ObterPorData(DateTime? data)
    {
        List<Pedido> pedidos = new List<Pedido>();
        
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido WHERE data = @data";

        cmd.Connection = MySqlConnection;
        cmd.Parameters.AddWithValue("@data", data);

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Pedido pedido = new Pedido(Convert.ToInt32(reader["id"]),
                Convert.ToDateTime(reader["data"]),
                Convert.ToString(reader["cliente"]),
                Convert.ToString(reader["status"])
            );

            pedidos.Add(pedido);
        }
        
        MySqlConnection.Close();
        return pedidos;
    }
    
    public void ImprimirTodos()
    {
        var listPedidos = ObterTodos("pedido");
        foreach (var pedido in listPedidos)
        {
            Console.WriteLine(pedido);
        }
    }
}