namespace Gamification03.Model;

public class Pedido
{
    public Pedido()
    {
    }

    public int Id { get; set; }
    public DateTime? Data { get; set; }
    public string? Cliente { get; set; }
    public string? Status { get; set; }

    public Pedido(int id, DateTime? data, string? cliente, string? status)
    {
        if (string.IsNullOrWhiteSpace(cliente))
        {
            throw new ArgumentException("Nome do cliente não pode ser vazio ou nulo.");
        }
        if (string.IsNullOrWhiteSpace(status))
        {
            throw new ArgumentException("Status não pode ser vazio ou nulo.");
        }

        Id = id;
        Data = data;
        Cliente = cliente;
        Status = status;
    }

    public Pedido(DateTime? data, string? cliente, string? status)
    {
        if (string.IsNullOrWhiteSpace(cliente))
        {
            throw new ArgumentException("Nome do cliente não pode ser vazio ou nulo.");
        }
        if (string.IsNullOrWhiteSpace(status))
        {
            throw new ArgumentException("Status não pode ser vazio ou nulo.");
        }
        
        Data = data;
        Cliente = cliente;
        Status = status;
    }

    public override string ToString()
    {
        return "[" + Id + "]\nData: " + Data + "\nCliente: " + Cliente + "\nStatus: " + Status;
    }
}