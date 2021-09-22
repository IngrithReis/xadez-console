using Xadrez;
using tabuleiro;
using System;


namespace Xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                PartidaDeXadrez partida = new PartidaDeXadrez();
                

                while (!partida.Terminada)
                {
                   

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    bool[,] posicoesPossiveis = partida.tab.Peca(origem).MovPossiveis();
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ExecutarMovimento(origem, destino);

                }
                
                
               
               

                
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine("Erro: " + e.Message);
            }
        }

    }
}
