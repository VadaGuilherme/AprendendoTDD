using AprendendoTDD.Dominio.Cursos;

namespace AprendendoTDD.DominioTest._Builders
{
    public class AprendendoTDDBuilder
    {
        private string _nome = "Informática básica";
        private double _cargaHoraria = 80;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valor = 950;
        private string _descricao = "Uma descrição";

        public static AprendendoTDDBuilder Novo()
        {
            return new AprendendoTDDBuilder();
        }

        public AprendendoTDDBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AprendendoTDDBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public AprendendoTDDBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public AprendendoTDDBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public AprendendoTDDBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
        }
    }
}
