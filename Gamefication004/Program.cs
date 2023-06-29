using Gamification03.Services;

public static class Program
{
    public static void Main()
        {
            GerenciamentoDePedidos gerenciamento = new GerenciamentoDePedidos();
            
            int sair = 0;

            while (sair != 1)
            {
                switch (Menu())
                {
                    case 1:
                        gerenciamento.CriarPedido();
                        Console.Clear();
                        break;
                    case 2:
                        gerenciamento.AdicionarItemPedidos();
                        Console.Clear();
                        break;
                    case 3:
                        gerenciamento.AtualizarStatus();
                        Console.Clear();
                        break;
                    case 4:
                        gerenciamento.RemoverPedido();
                        Console.Clear();
                        break;
                    case 5:
                        switch (MenuBusca())
                        {
                            case 1:
                                gerenciamento.ListarPedidos("Cliente");
                                Console.Clear();
                                break;
                            case 2:
                                gerenciamento.ListarPedidos("Data");
                                Console.Clear();
                                break;
                            case 3:
                                gerenciamento.ListarPedidos("Status");
                                Console.Clear();
                                break;
                        }
                        break;
                    case 6:
                        gerenciamento.CalcularValorPedido();
                        Console.Clear();
                        break;
                    default:
                        sair = 1;
                        break;
                }
            }
        }

        public static int Menu()
        {
            Console.WriteLine("\n-=: Digite a opção desejada :=-");
            Console.WriteLine("1 - Adicionar novo pedido");
            Console.WriteLine("2 - Adicionar itens a um pedido");
            Console.WriteLine("3 - Atualizar status pedido");
            Console.WriteLine("4 - Remover pedido");
            Console.WriteLine("5 - Listar pedido com filtro");
            Console.WriteLine("6 - Calcular valor de pedido");
            Console.WriteLine("0 - Sair");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static int MenuBusca()
        {
            Console.WriteLine("\n-=: Digite a opção desejada :=-");
            Console.WriteLine("1 - Buscar por cliente");
            Console.WriteLine("2 - Buscar por data");
            Console.WriteLine("3 - Buscar por status");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
