using System;
using tabuleiro;

namespace Xadrez_Console
{
    class Tela
    { public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int l = 0; l < tab.Linhas; l++) // varre primeiro cada coluna de acordo com a quantidade de linhas. coluna, coluna, coluna...
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c< tab.Colunas; c++)
                { 
                    if (tab.PecaPosicao(l, c) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tab.PecaPosicao(l,c));
                        Console.Write(" ");
                    }
                   
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Branca)
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
        }
    }
}
