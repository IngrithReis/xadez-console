using System;
using tabuleiro;
using Xadrez;

namespace Xadrez_Console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] possicoesPossiveis)
        {
            ConsoleColor corOriginal = Console.BackgroundColor;
            ConsoleColor corAlterada = ConsoleColor.DarkGray;

            for (int l = 0; l < tab.Linhas; l++) // varre primeiro cada coluna de acordo com a quantidade de linhas. coluna, coluna, coluna...
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tab.Colunas; c++)
                {
                    if (possicoesPossiveis[l, c])
                    {
                        Console.BackgroundColor = corAlterada;
                    }
                    else
                    {
                        Console.BackgroundColor = corOriginal;
                    }

                    ImprimirPeca(tab.PecaPosicao(l, c));
                    Console.BackgroundColor = corOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = corOriginal;
        }
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
           
            for (int l = 0; l < tab.Linhas; l++) // varre primeiro cada coluna de acordo com a quantidade de linhas. coluna, coluna, coluna...
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tab.Colunas; c++)
                {
                    ImprimirPeca(tab.PecaPosicao(l, c));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            
        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + " ");
            return new PosicaoXadrez(coluna, linha);
        }



        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
