using AprendendoTDD.Dominio.Cursos;
using AprendendoTDD.DominioTest._Builders;
using AprendendoTDD.DominioTest._Util;
using Bogus;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace AprendendoTDD.DominioTest.Cursos
{
    //Eu, enquanto administrador, quero criar e editar cursos para que sejam abertas matriculas para o mesmo

    //Critério de Aceite

    //- Criar um curso com nome, carga horária, público alvo e valor do curso.
    //- As opções para publico alvo são: Estudante, Universitário, Empregado e Empreendedor.
    //- Todos os campos do curso são obrigatórios.
    //- Curso deve ter uma descricao.

    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado");
            var faker = new Faker();

            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100, 1000);
            _descricao = faker.Lorem.Paragraph();
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }

        [Fact(DisplayName = "DeveCriarCurso")]        
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, 
                                  cursoEsperado.CargaHoraria, 
                                  cursoEsperado.PublicoAlvo, 
                                  cursoEsperado.Valor,
                                  cursoEsperado.Descricao);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                        AprendendoTDDBuilder.Novo().ComNome(nomeInvalido).Build())
            .ComMensagem("Nome inválido");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerCargaHorariaMenorQueUm(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                       AprendendoTDDBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
            .ComMensagem("Carga Horaria inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerUmValorMenorQueUm(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                       AprendendoTDDBuilder.Novo().ComValor(valorInvalido).Build())
            .ComMensagem("Valor inválido");
        }
    }
}
