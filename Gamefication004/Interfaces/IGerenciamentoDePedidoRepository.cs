namespace Gamification03.Interfaces;

public interface IGerenciamentoDePedidoRepository
{
    void CriarPedido();
    void AdicionarItemPedidos();
    void AtualizarStatus();
    void RemoverPedido();
    void ListarPedidos(string filtro);
    void CalcularValorPedido();
}