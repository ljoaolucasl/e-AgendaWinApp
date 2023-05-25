﻿using e_Agenda.WinApp.Compartilhado;

namespace e_Agenda.WinApp.ModuloDespesas
{
    public partial class ListagemDespesaControl : UserControl, ITabelaBase<Despesa>
    {
        public ListagemDespesaControl()
        {
            InitializeComponent();
        }

        public DataGridView DataGridView { get; }

        public void AtualizarLista(List<Despesa> despesas)
        {
            listDespesas.Items.Clear();

            foreach (Despesa despesa in despesas)
            {
                ListViewItem item = new ListViewItem(despesa.id.ToString());
                item.SubItems.Add(despesa.descricao);
                item.SubItems.Add(despesa.valor);
                item.SubItems.Add(despesa.data);
                item.SubItems.Add(despesa.formaPagamento);
                item.Tag = despesa;
                listDespesas.Items.Add(item);
            }

            TelaPrincipalForm.AtualizarStatus($"Visualizando {despesas.Count} Despesas");
        }

        public Despesa? ObterTarefaSelecionada()
        {
            ListViewItem itemSelecionado = listDespesas.SelectedItems[0];
            return listDespesas.SelectedItems.Count > 0 ? (Despesa)itemSelecionado.Tag : null;
        }
    }
}
