using System;
using tabuleiro;

namespace Xadrez_Console
{
    class Tela
    { public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int l = 0; l < tab.Linhas; l++) // varre primeiro cada coluna de acordo com a quantidade de linhas. coluna, coluna, coluna...
            {   
                for(int c = 0; c< tab.Colunas; c++)
                {
                    if (tab.PecaPosicao(l, c) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tab.PecaPosicao(l, c) + " ");
                    }
                   
                }
                Console.WriteLine();
            }
            
        }
    }
}
