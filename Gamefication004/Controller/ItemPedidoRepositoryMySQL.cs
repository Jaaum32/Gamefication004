using Gamification03.Interfaces;
using Gamification03.Model;
using MySql.Data.MySqlClient;

namespace Gamification03.Controller;

public class ItemPedidoRepositoryMySql : IItemPedidoRepository
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
    
    public void Adicionar(ItemPedido itemPedido)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText =
            "INSERT INTO ITEMPEDIDO(produto, quantidade, preco_unit, pedido_id) VALUES(@produto, @quantidade, @preco_unit, @pedido_id); SELECT LAST_INSERT_ID();";

        cmd.Parameters.AddWithValue("@produto", itemPedido.Produto);
        cmd.Parameters.AddWithValue("@quantidade", itemPedido.Quantidade);
        cmd.Parameters.AddWithValue("@preco_unit", itemPedido.PrecoUnit);
        cmd.Parameters.AddWithValue("@pedido_id", itemPedido.PedidoId);

        cmd.Connection = _mySqlConnection;

        itemPedido.Id = Convert.ToInt32(cmd.ExecuteScalar());
        cmd.ExecuteReader();
        _mySqlConnection.Close();
    }

    public void Atualizar(ItemPedido itemPedido)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "UPDATE SET produto = @produto, quantidade = @quantidade, preco_unit = @preco_unit, pedido_id = @pedido_idWHERE id = @id";

        cmd.Parameters.AddWithValue("@produto", itemPedido.Produto);
        cmd.Parameters.AddWithValue("@quantidade", itemPedido.Quantidade);
        cmd.Parameters.AddWithValue("@preco_unit", itemPedido.PrecoUnit);
        cmd.Parameters.AddWithValue("@pedido_id", itemPedido.PedidoId);
        cmd.Parameters.AddWithValue("@id", itemPedido.Id);

        cmd.Connection = _mySqlConnection;
        cmd.ExecuteReader();
        _mySqlConnection.Close();
    }

    public void Excluir(int id)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "DELETE * FROM ItemPedido WHERE id = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.Connection = _mySqlConnection;
        cmd.ExecuteReader();
        _mySqlConnection.Close();
    }

    public ItemPedido ObterPorId(int id)
    {
        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM pedido WHERE id = @id";

        cmd.Connection = _mySqlConnection;
        cmd.Parameters.AddWithValue("@id", id);

        var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new ItemPedido(Convert.ToInt32(reader["id"]),
                Convert.ToString(reader["produto"]),
                Convert.ToInt32(reader["quantidade"]),
                Convert.ToDouble(reader["preco_unit"]),
                Convert.ToInt32(reader["pedido_id"])
            );
        }
        
        _mySqlConnection.Close();
        return null!;
    }

    public IEnumerable<ItemPedido> ListarTodos()
    {
        List<ItemPedido> itemPedidos = new List<ItemPedido>();

        InicializeDatabase();
        MySqlCommand cmd = new MySqlCommand();

        cmd.CommandText = "SELECT * FROM ItemPedido";

        cmd.Connection = _mySqlConnection;
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
