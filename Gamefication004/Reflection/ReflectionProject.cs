using System.Data.SqlTypes;
using System.Reflection;
using System.Text.Json;
using Gamification03.Controller;
using Gamification03.Model;

namespace projeto_final_POO2.reflection;
using System;
using System.IO;

public class CreateTxt
{
    private ItemPedidoRepositoryMySql _itemPedidoRepositoryMySql = new ItemPedidoRepositoryMySql();
    private PedidoRepositoryMySql _pedidoRepositoryMySql = new PedidoRepositoryMySql();

    public void createTxt()
    {
        IEnumerable<Pedido> pedidos = _pedidoRepositoryMySql.ObterTodos("pedido");
        IEnumerable<ItemPedido> itensPedidos = _itemPedidoRepositoryMySql.ObterTodos("itempedido");
        
        StreamWriter file = File.CreateText("C:\\Users\\erick\\Desktop\\Arquivo.txt");

        foreach (var pedido in pedidos)
        {
            SaveObjectToJson(pedido, "C:\\Users\\erick\\Desktop\\json.txt");
            file.WriteLine(pedido.ToString());
        }
        
        foreach (var itemPedido in itensPedidos)
        {
            SaveObjectToJson(itemPedido, "C:\\Users\\erick\\Desktop\\json.txt");
            file.WriteLine(itemPedido.ToString());
        }
        
        file.Close();

        Console.WriteLine("Arquivo de texto criado com sucesso!");
    }
    
    public void SaveObjectToJson<T>(T obj, string filePath)
    {
        if (obj == null)
        {
            throw new ArgumentNullException("obj");
        }

        string jsonString = JsonSerializer.Serialize(obj);

        File.AppendAllText(filePath, jsonString + Environment.NewLine);
    }
}