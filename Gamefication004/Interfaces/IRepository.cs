using System.Collections;

namespace Gamification03.Interfaces;

public interface IRepository<T> 
{
    void Inserir(string tableName, T entidade);
    void Atualizar(string tableName, T entidade); 
    void Excluir(string tableName, int id);
    T? ObterPorId(string tableName, int id);
    IEnumerable<T?> ObterTodos(string tableName);

}