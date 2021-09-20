using Xadrez;
using tabuleiro;


namespace Xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(1, 3));
                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(9, 0));


                Tela.ImprimirTabuleiro(tab);
            }
            catch(TabuleiroException tb)
            {
                System.Console.WriteLine("Erro: " + tb.Message);
            }
        }

    }
}
