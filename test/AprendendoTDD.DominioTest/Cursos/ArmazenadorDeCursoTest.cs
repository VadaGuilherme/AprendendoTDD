using AprendendoTDD.Dominio.Cursos;
using AprendendoTDD.DominioTest._Util;
using Bogus;
using Moq;
using System;
using Xunit;

namespace AprendendoTDD.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDTO _cursoDTO;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorDeCursoTest()
        {
            var faker = new Faker();
            _cursoDTO = new CursoDTO
            {
                Nome = faker.Random.Words(),
                CargaHoraria = faker.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = faker.Random.Double(1000, 2000),
                Descricao = faker.Lorem.Paragraph()
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();

            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDTO);

            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c =>
                        c.Nome == _cursoDTO.Nome &&
                        c.Descricao == _cursoDTO.Descricao
                )
            )
            //Times.AtLeast(2)
            //Times.Never
            );
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDTO.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
                .ComMensagem("Publico Alvo inválido");
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
            Enum.TryParse(typeof(PublicoAlvo), cursoDTO.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
                throw new ArgumentException("Publico Alvo inválido");

            var curso = new Curso(cursoDTO.Nome,
                                cursoDTO.CargaHoraria,
                                (PublicoAlvo)publicoAlvo,
                                cursoDTO.Valor,
                                cursoDTO.Descricao
                                );

            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDTO
    {
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}
