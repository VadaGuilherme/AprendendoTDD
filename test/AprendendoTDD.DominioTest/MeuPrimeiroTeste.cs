using Xunit;

namespace AprendendoTDD.DominioTest
{

    public class MeuPrimeiroTeste
    {
        [Fact(DisplayName = "DeveVariavel1SerIgualVariavel2")]
        public void DeveVariavel1SerIgualVariavel2()
        {
            //Organização
            var variavel1 = 1;
            var variavel2 = 1;

            //Ação
            variavel2 = variavel1;

            //Assert
            Assert.Equal(variavel1, variavel2);
        }
    }
}
