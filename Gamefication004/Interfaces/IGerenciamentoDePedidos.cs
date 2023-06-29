using Gamification03.Model;

namespace Gamification03.Interfaces;

public interface IGerenciamentoDePedidos
{
    void criarNovoPedido();
    void adicionarItensPedido(List<ItemPedido> itensPedido);
    void atualizarStatusPedido();
    void removerPedido();
    void listarPorParametro();
    void calcularValorTotalPedido();
}