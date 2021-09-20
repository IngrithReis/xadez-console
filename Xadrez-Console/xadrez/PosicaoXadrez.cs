using tabuleiro;

namespace Xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int lina)
        {
            Coluna = coluna;
            Linha = lina;
        }
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna -'a');
        }
        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
