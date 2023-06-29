using Gamefication004.Generics;
using Gamification03.Interfaces;
using Gamification03.Model;
using MySql.Data.MySqlClient;

namespace Gamification03.Controller;

public class ItemPedidoRepositoryMySql : Repository<ItemPedido> 
{
    private MySqlConnection _mySqlConnection = new MySqlConnection("Persist Security Info=False;server=localhost;database=gamefication;uid=root;pwd=0406");
    private void InicializeDatabase()
    {
        try{
            //abre a conexao
            _mySqlConnection.Open();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    public IEnumerable<ItemPedido> ListarTodosPorId(int pedidoId)
    {
        List<ItemPedido> itemPedidos = new List<ItemPedido>();

        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM ItemPedido WHERE pedido_id = @pedido_id";

        cmd.Connection = _mySqlConnection;
        cmd.Parameters.AddWithValue("@pedido_id", pedidoId);
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            ItemPedido itemPedido = new ItemPedido(Convert.ToInt32(reader["id"]),
                Convert.ToString(reader["produto"]),
                Convert.ToInt32(reader["quantidade"]),
                Convert.ToDouble(reader["preco_unit"]),
                Convert.ToInt32(reader["pedido_id"])
            );

            itemPedidos.Add(itemPedido);
        }
        
        _mySqlConnection.Close();
        return itemPedidos;
    }
}