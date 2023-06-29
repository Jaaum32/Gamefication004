using System.Globalization;
using System.Linq.Expressions;
using Gamification03.Controller;
using Gamification03.Model;

namespace Gamification03.Services;

using Interfaces;

public class GerenciamentoDePedidos : IGerenciamentoDePedidoRepository
{
    private readonly PedidoRepositoryMySql _pedidoRepositoryMySqlr = new PedidoRepositoryMySql();
    private readonly ItemPedidoRepositoryMySql _itemPedidoRepositoryMySqlpr = new ItemPedidoRepositoryMySql();

    public void CriarPedido()
    {
        Console.WriteLine("PREENCHA OS DADOS DO PEDIDO:");
        Console.Write("Data do pedido (dd/mm/aaaa): ");
        DateTime dataPedido = readData();

        Console.Write("Cliente: ");
        string? cliente = Console.ReadLine();

        Console.Write("Status [Entregue|Enviado|Pendente]: ");
        string? status = Console.ReadLine();

        Pedido pedido = new Pedido(dataPedido, cliente, status);

        _pedidoRepositoryMySqlr.Inserir("pedido", pedido);
    }

    public void AdicionarItemPedidos()
    {
        _pedidoRepositoryMySqlr.ImprimirTodos();
        Console.WriteLine("PREENCHA OS DADOS DO ITEM:");
        Console.Write("ID do pedido: ");
        int pedidoId;
        while (true)
        {
            try
            {
                pedidoId = readInt();
                if (_pedidoRepositoryMySqlr.ObterPorId("pedido", pedidoId) == null)
                    Console.WriteLine("Nenhum pedido com esse ID!");
                else
                    break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

        Console.Write("Nome do produto: ");
        string? produto = Console.ReadLine();

        Console.Write("Quantidade: ");
        int quantidade = readInt();

        Console.Write("Preço unitário: ");
        decimal precoUnitario = readDouble();

        ItemPedido itemPedido = new ItemPedido(produto, quantidade, precoUnitario, pedidoId);

        _itemPedidoRepositoryMySqlpr.Inserir("itempedido", itemPedido);
    }

    public void AtualizarStatus()
    {
        _pedidoRepositoryMySqlr.ImprimirTodos();
        Console.Write("ID do pedido: ");
        int pedidoId;
        while (true)
        {
            try
            {
                pedidoId = readInt();
                if (_pedidoRepositoryMySqlr.ObterPorId("pedido", pedidoId) == null)
                    Console.WriteLine("Nenhum pedido com esse ID!");
                else
                    break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

        Console.Write("Status [Entregue|Enviado|Pendente]: ");
        string? status = readStatus();

        _pedidoRepositoryMySqlr.AtualizarStatus(pedidoId, status);
    }

    public void RemoverPedido()
    {
        _pedidoRepositoryMySqlr.ImprimirTodos();
        Console.WriteLine("Digite o ID do pedido para excluir:");
        int pedidoId;
        while (true)
        {
            try
            {
                pedidoId = readInt();
                if (_pedidoRepositoryMySqlr.ObterPorId("pedido", pedidoId) == null)
                    Console.WriteLine("Nenhum pedido com esse ID! Digite outro");
                else
                    break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
        }

        _pedidoRepositoryMySqlr.Excluir("pedido", pedidoId);
    }

    public void ListarPedidos(string filtro)
    {
        IEnumerable<Pedido> pedidos = new List<Pedido>();

        if (filtro == "Cliente")
        {
            Console.Write("Cliente: ");
            string? cliente = Console.ReadLine();

            if (cliente != null)
                pedidos = _pedidoRepositoryMySqlr.ObterPorNome(cliente);
        }

        else if (filtro == "Status")
        {
            Console.Write("Status [Entregue|Enviado|Pendente]: ");
            string? status = readStatus();

            if (status != null)
                pedidos = _pedidoRepositoryMySqlr.ObterPorStatus(status);
        }

        else if (filtro == "Data")
        {
            Console.Write("Data do pedido (dd/mm/aaaa): ");
            DateTime? dataPedido = readData();

            if (dataPedido != null)
                pedidos = _pedidoRepositoryMySqlr.ObterPorData(dataPedido);
        }

        if (pedidos.Count() == 0)
            Console.WriteLine("Nenhum pedido com esses dados!");
        else
        {
            
            Console.WriteLine("\n Pedidos: \n");
            foreach (var pedido in pedidos)
            {
                
                Console.WriteLine(pedido.ToString());
                IEnumerable<ItemPedido> itens = _itemPedidoRepositoryMySqlpr.ListarTodosPorId(pedido.Id);
                Console.WriteLine("\nItens: \n");
                foreach (var item in itens)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
    }

    public void CalcularValorPedido()
    {
        decimal sum = 0;

        Console.Write("ID do pedido: ");
        int pedidoId = readInt();

        IEnumerable<ItemPedido> itens = _itemPedidoRepositoryMySqlpr.ListarTodosPorId(pedidoId);

        foreach (var item in itens)
        {
            sum += item.Quantidade * item.Preco;
        }

        Console.WriteLine("Valor Total Pedido: " + sum);
    }

    public int readInt()
    {
        int x;
        while (!int.TryParse(Console.ReadLine(), out x))
        {
            Console.WriteLine("Digite um valor válido!");
        }

        return x;
    }

    public decimal readDouble()
    {
        decimal x;
        while (!decimal.TryParse(Console.ReadLine(), out x))
        {
            Console.WriteLine("Digite um valor válido!");
        }

        return x;
    }

    public string? readStatus()
    {
        string? x = "";
        while (x != "Entregue" && x != "Enviado" && x != "Pendente")
        {
            x = Console.ReadLine();
            if (x != "Entregue" && x != "Enviado" && x != "Pendente")
            {
                Console.WriteLine("Status inválido!");
            }
        }

        return x;
    }

    public DateTime readData()
    {
        DateTime dataValida;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out dataValida))
        {
            Console.WriteLine("Digite uma data válida!");
        }

        return dataValida;
    }
}