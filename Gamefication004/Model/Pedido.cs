namespace Gamification03.Model;

public class Pedido
{
    public int Id { get; set; }
    public string? Data { get; set; }
    public string? Cliente { get; set; }
    public string? Status { get; set; }

    public Pedido(int id, string? data, string? cliente, string? status)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Data não pode ser vazio ou nulo.");
        }
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

    public Pedido(string? data, string? cliente, string? status)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Data não pode ser vazio ou nulo.");
        }
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
        return "ID do Pedido: " + Id + "\nData: " + Data + "\nCliente: " + Cliente + "\nStatus: " + Status;
    }
}