

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }

        public int Colunas { get; set; }

        private Peca[,] pecas; // apenas o tabuleiro poderá mexer nelas

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        public Peca PecaPosicao(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

    }
}
