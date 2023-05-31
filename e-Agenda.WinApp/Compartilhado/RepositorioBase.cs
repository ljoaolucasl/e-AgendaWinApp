﻿namespace e_Agenda.WinApp.Compartilhado
{
    [Serializable]
    public abstract class RepositorioBase<TEntidade> where TEntidade : Entidade<TEntidade>
    {
        private List<TEntidade> listaRegistros = new();

        private int id = 1001;

        private string CaminhoArquivo => $"{typeof(TEntidade).Name}.bin";

        public int Id { get { return id; } }

        public void Adicionar(TEntidade registro)
        {
            registro.id = id; id++;
            listaRegistros.Add(registro);
            RepositorioGlobal.GravarRegistrosEmArquivoBIN();
        }

        public void Editar(TEntidade novoRegistro)
        {
            TEntidade registroAntigo = SelecionarId(novoRegistro.id);

            foreach (var atributo in registroAntigo.GetType().GetFields())
            {
                if (atributo.Name != "id")
                    atributo.SetValue(registroAntigo, atributo.GetValue(novoRegistro));
            }

            foreach (var property in registroAntigo.GetType().GetProperties())
            {
                if (property.Name != "Id")
                    property.SetValue(registroAntigo, property.GetValue(novoRegistro));
            }

            RepositorioGlobal.GravarRegistrosEmArquivoBIN();
        }

        public void Excluir(TEntidade registroSelecionado)
        {
            listaRegistros.Remove(registroSelecionado);

            RepositorioGlobal.GravarRegistrosEmArquivoBIN();
        }

        public TEntidade SelecionarId(int idEscolhido)
        {
            return listaRegistros.Find(e => e.id == idEscolhido);
        }

        public List<TEntidade> ObterListaRegistros()
        {
            return listaRegistros;
        }
    }
}
