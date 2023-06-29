using Gamification03.Model;
namespace Gamification03.Interfaces;

public interface IPedidoRepository
{
    void Adicionar(Pedido pedido);
    void Atualizar(Pedido pedido);
    void Excluir(int id);
    Pedido ObterPorId(int id);
    IEnumerable<Pedido> ListarTodos();
    
}