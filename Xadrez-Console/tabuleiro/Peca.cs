namespace tabuleiro
{
        abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; } // protected, pode ser alterada por ela mesma e pelas subclasses dela

        public int QtMovi { get; protected set; } // poderá ser alterada por ela mesma e pelas subclasses

        public Tabuleiro Tab { get; protected set; }

        public Peca( Cor cor, Tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QtMovi = 0;
        }

        public void IncrementarQtMovimentos()
        {
            QtMovi++;
        }
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovPossiveis();
            for(int l = 0; l < Tab.Linhas; l++)
            {
               for (int c = 0; c < Tab.Colunas; c++)
                {
                    if(mat[l,c] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool PodeMoverPara(Posicao pos)
        {
            return MovPossiveis()[pos.Linha, pos.Coluna];
        }
        public abstract bool[,] MovPossiveis();
        


    }
}
