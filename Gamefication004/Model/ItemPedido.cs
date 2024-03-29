namespace Gamification03.Model;

public class ItemPedido
{
    public ItemPedido()
    {
    }

    public int Id { get; set; }
    public string? Produto { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }
    public int PedidoId { get; set; }

    public ItemPedido(int id, string? produto, int quantidade, decimal preco, int pedidoId)
    {
        if (string.IsNullOrWhiteSpace(produto))
        {
            throw new ArgumentException("Nome do produto não pode ser vazio ou nulo.");
        }
        if (quantidade <= 0)
        {
            throw new ArgumentException("Quantidade do produto deve ser maior que zero.");
        }
        if (preco <= 0)
        {
            throw new ArgumentException("Preço unitário deve ser maior que zero.");
        }
        
        Id = id;
        Produto = produto;
        Quantidade = quantidade;
        Preco = preco;
        PedidoId = pedidoId;
    }

    public ItemPedido(string? produto, int quantidade, decimal preco, int pedidoId)
    {
        if (string.IsNullOrWhiteSpace(produto))
        {
            throw new ArgumentException("Nome do produto não pode ser vazio ou nulo.");
        }
        if (quantidade <= 0)
        {
            throw new ArgumentException("Quantidade do produto deve ser maior que zero.");
        }
        if (preco <= 0)
        {
            throw new ArgumentException("Preço unitário deve ser maior que zero.");
        }
        
        Produto = produto;
        Quantidade = quantidade;
        Preco = preco;
        PedidoId = pedidoId;
    }

    public override string ToString()
    {
        return "[" + Id + "]\nNome: " + Produto + "\nQuantidade: " + Quantidade + "\nPreço Unit: " + Preco;
    }
}