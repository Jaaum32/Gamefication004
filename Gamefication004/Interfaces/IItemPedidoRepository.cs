using Gamification03.Model;
namespace Gamification03.Interfaces;


public interface IItemPedidoRepository
{
    void Adicionar(ItemPedido itemPedido);
    void Atualizar(ItemPedido itemPedido);
    void Excluir(int id);
    ItemPedido ObterPorId(int id);
    IEnumerable<ItemPedido> ListarTodos();
}