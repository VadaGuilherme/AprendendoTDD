using AprendendoTDD.Dominio.Cursos;
using Moq;
using System;
using Xunit;

namespace AprendendoTDD.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDTO = new CursoDTO
            {
                Nome = "Curso A",
                CargaHoraria = 80,
                PublicoAlvoId = 1,
                Valor = 950,
                Descricao = "Descrição,"
            };

            var cursoRepositorioMock = new Mock<ICursoRepositorio>();

            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);

            armazenadorDeCurso.Armazenar(cursoDTO);

            cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDTO cursoDTO)
        {
            var curso = new Curso(cursoDTO.Nome,
                                cursoDTO.CargaHoraria,
                                PublicoAlvo.Estudante,
                                cursoDTO.Valor,
                                cursoDTO.Descricao
                                );

            _cursoRepositorio.Adicionar(curso);
        }
    }

    public  class CursoDTO
    {
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int PublicoAlvoId { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}
