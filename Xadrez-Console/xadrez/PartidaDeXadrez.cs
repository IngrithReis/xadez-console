﻿using tabuleiro;
using System.Collections.Generic;
using xadrez;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }

        public Cor jogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        private HashSet<Peca> pecas;

        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarQtMovimentos();
                tab.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarQtMovimentos();
                tab.ColocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.RetirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecreementarQtMovimentos();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.DecreementarQtMovimentos();
                tab.ColocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.DecreementarQtMovimentos();
                tab.ColocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    tab.ColocarPeca(peao, posP);
                }
            }
        }
    
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);
            if (EstaEmCheque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }
            Peca p = tab.Peca(destino);

            // #jogadaespecial promocao
            if (p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = tab.RetirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.Cor);
                    tab.ColocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }
            var a = Coradversaria(jogadorAtual);
            var b = EstaEmCheque(a);

            if (b)
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (EstaEmCheque(Coradversaria(jogadorAtual)))
            {
                Terminada = true;
            }
            else
            {

                turno++;
                MudaJogador();
            }
            

            // #jogadaespecial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem informada!");
            }
            if (jogadorAtual != tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("Peça de origem escolhida não é sua! ");
            }
            if (!tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida! ");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida! ");
            }

        }
        private void MudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }

        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmCheque(Cor cor)
        {
            Peca r = Rei(cor);
            if (r == null)
            {
                throw new TabuleiroException("Não há rei" + cor + " no tabuleiro! ");
            }

            var a = PecasEmJogo(Coradversaria(cor));
            foreach (Peca x in a) 
            {
                bool[,] mat = x.MovPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmCheque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovPossiveis();
                for (int l = 0; l < tab.Linhas; l++)
                {
                    for (int c = 0; c < tab.Colunas; c++)
                    {
                        if (mat[l, c])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(l, c);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmCheque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        private Cor Coradversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        public void ColocarNovapeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);

        }

        private void ColocarPecas()

        {
            ColocarNovapeca('a', 1, new Torre(tab, Cor.Branca));
            ColocarNovapeca('b', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovapeca('c', 1, new Bispo(tab, Cor.Branca));
            ColocarNovapeca('d', 1, new Dama(tab, Cor.Branca));
            ColocarNovapeca('e', 1, new Rei(tab, Cor.Branca, this));
            ColocarNovapeca('f', 1, new Bispo(tab, Cor.Branca));
            ColocarNovapeca('g', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovapeca('h', 1, new Torre(tab, Cor.Branca));
            ColocarNovapeca('a', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovapeca('b', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovapeca('c', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovapeca('d', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovapeca('e', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovapeca('f', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovapeca('g', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovapeca('h', 2, new Peao(tab, Cor.Branca, this));

            ColocarNovapeca('a', 8, new Torre(tab, Cor.Preta));
            ColocarNovapeca('b', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovapeca('c', 8, new Bispo(tab, Cor.Preta));
            ColocarNovapeca('d', 8, new Dama(tab, Cor.Preta));
            ColocarNovapeca('e', 8, new Rei(tab, Cor.Preta, this));
            ColocarNovapeca('f', 8, new Bispo(tab, Cor.Preta));
            ColocarNovapeca('g', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovapeca('h', 8, new Torre(tab, Cor.Preta));
            ColocarNovapeca('a', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovapeca('b', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovapeca('c', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovapeca('d', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovapeca('e', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovapeca('f', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovapeca('g', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovapeca('h', 7, new Peao(tab, Cor.Preta, this));





            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            //tab.ColocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            // tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());


            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            //tab.ColocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());



        }

    }
}
