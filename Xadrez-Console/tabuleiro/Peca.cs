

namespace tabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; } // protected, pode ser alterada por ela mesma e pelas subclasses dela

        public int QtMovi { get; protected set; } // poderá ser alterada por ela mesma e pelas subclasses

        public Tabuleiro Tab { get; protected set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tab)
        {
            Posicao = posicao;
            Cor = cor;
            Tab = tab;
            QtMovi = 0;
        }
    }
}
