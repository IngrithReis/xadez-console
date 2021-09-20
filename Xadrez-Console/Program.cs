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
               
                tab.ColocarPeca(new Torre(tab, Cor.Preta),new Posicao(0,0));
                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));

                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(7, 0));
                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(7, 2));

                Tela.ImprimirTabuleiro(tab);
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine("Erro: " + e.Message);
            }
        }

    }
}
