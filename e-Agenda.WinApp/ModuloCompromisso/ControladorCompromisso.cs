﻿using e_Agenda.WinApp.Compartilhado;
using e_Agenda.WinApp.ModuloContato;

namespace e_Agenda.WinApp.ModuloCompromisso
{
    public class ControladorCompromisso : ControladorBase<Compromisso, RepositorioCompromisso, ListagemCompromissoControl, TelaCompromissoForm, RepositorioContato>
    {
        private RepositorioCompromisso _repositorioCompromisso;
        private RepositorioContato _repositorioContato;
        private ListagemCompromissoControl _listagemCompromisso;

        public ControladorCompromisso(RepositorioCompromisso _repositorio, ListagemCompromissoControl _listagem, RepositorioContato _repositorio2) : base(_repositorio, _listagem, _repositorio2)
        {
            this._repositorioCompromisso = _repositorio;
            this._listagemCompromisso = _listagem;
            this._repositorioContato = _repositorio2;

            this.onCarregarArquivosSegundoRepositorio += CarregarComboBox;
        }

        public void CarregarComboBox(TelaCompromissoForm telaCompromisso)
        {
            telaCompromisso.cbContato.DisplayMember = "Nome";
            telaCompromisso.cbContato.ValueMember = "Nome";
            telaCompromisso.cbContato.DataSource = _repositorioContato.ObterListaRegistros();
        }

        public override void Filtrar()
        {
            TelaFiltroCompromissoForm telaFiltroCompromisso = new();

            TelaPrincipalForm.AtualizarStatus($"Filtrando Compromissos");

            DialogResult opcaoEscolhida = telaFiltroCompromisso.ShowDialog();

            if (opcaoEscolhida == DialogResult.OK)
            {
                FiltroCompromisso filtroSelecionado = telaFiltroCompromisso.FiltroSelecionado;
                DateTime dataInicial = telaFiltroCompromisso.DataInicial.Date;
                DateTime dataFinal = telaFiltroCompromisso.DataFinal.Date;

                CarregarCompromissosComFiltro(filtroSelecionado, dataInicial, dataFinal);
            }
        }

        private void CarregarCompromissosComFiltro(FiltroCompromisso filtroSelecionado, DateTime dataInicial, DateTime dataFinal)
        {
            List<Compromisso> listaFiltrada = new List<Compromisso>();

            switch (filtroSelecionado)
            {
                case FiltroCompromisso.Todos: listaFiltrada = _repositorioCompromisso.ObterListaRegistros();
                    break;
                case FiltroCompromisso.Passados: listaFiltrada = _repositorioCompromisso.FiltrarCompromissosPassados(DateTime.Now);
                    break;
                case FiltroCompromisso.Futuros: listaFiltrada = _repositorioCompromisso.FiltrarCompromissosFuturos(dataInicial, dataFinal);
                    break;
            }

            _listagemCompromisso.AtualizarLista(listaFiltrada);
        }

        public override ListagemCompromissoControl ObterListagem()
        {
            return _listagem;
        }
    }
}
