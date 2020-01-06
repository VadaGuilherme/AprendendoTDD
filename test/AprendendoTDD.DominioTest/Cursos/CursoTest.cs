using AprendendoTDD.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;

namespace AprendendoTDD.DominioTest.Cursos
{
    //Eu, enquanto administrador, quero criar e editar cursos para que sejam abertas matriculas para o mesmo

    //Critério de Aceite

    //- Criar um curso com nome, carga horária, público alvo e valor do curso.
    //- As opções para publico alvo são: Estudante, Universitário, Empregado e Empreendedor.
    //- Todos os campos do curso são obrigatórios.

    public class CursoTest
    {
        [Fact(DisplayName = "DeveCriarCurso")]        
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
        };

            var curso = new Curso(cursoEsperado.Nome, 
                                  cursoEsperado.CargaHoraria, 
                                  cursoEsperado.PublicoAlvo, 
                                  cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() =>
                        new Curso(nomeInvalido,
                                  cursoEsperado.CargaHoraria,
                                  cursoEsperado.PublicoAlvo,
                                  cursoEsperado.Valor))
            .ComMensagem("Nome inválido");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerCargaHorariaMenorQueUm(double cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() =>
                       new Curso(cursoEsperado.Nome,
                                  cargaHorariaInvalida,
                                  cursoEsperado.PublicoAlvo,
                                  cursoEsperado.Valor))
            .ComMensagem("Carga Horaria inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerUmValorMenorQueUm(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            Assert.Throws<ArgumentException>(() =>
                        new Curso(cursoEsperado.Nome,
                                  cursoEsperado.CargaHoraria,
                                  cursoEsperado.PublicoAlvo,
                                  valorInvalido))
            .ComMensagem("Valor inválido");
        }

        //[Fact]
        //public void CursoNaoDeveTerNomeNulo()
        //{
        //    var cursoEsperado = new
        //    {
        //        Nome = "Informática básica",
        //        CargaHoraria = (double)80,
        //        PublicoAlvo = PublicoAlvo.Estudante,
        //        Valor = (double)950
        //    };

        //    Assert.Throws<ArgumentException>(() =>
        //                new Curso(null,
        //                          cursoEsperado.CargaHoraria,
        //                          cursoEsperado.PublicoAlvo,
        //                          cursoEsperado.Valor));
        //}
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitário,
        Empregado,
        Empreendedor
    }

    internal class Curso
    {
        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");

            if(cargaHoraria < 1)
                throw new ArgumentException("Carga Horaria inválido");

            if(valor < 1)
                throw new ArgumentException("Valor inválido");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
    }
}
