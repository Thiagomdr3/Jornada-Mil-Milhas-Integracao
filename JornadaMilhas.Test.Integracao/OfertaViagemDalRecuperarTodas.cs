using JornadaMilhas.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integracao
{
    [Collection(nameof(ContextCollection))]
    public class OfertaViagemDalRecuperarTodas
    {
        private readonly JornadaMilhasContext _context;
        private readonly ContextoFixture _fixture;
        public OfertaViagemDalRecuperarTodas(ITestOutputHelper output, ContextoFixture fixture)
        {
              _context = fixture.Context;
            output.WriteLine(_context.GetHashCode().ToString());
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture.LimpaDadosDoBanco();
        }
        [Fact]
        public void RetornaTodasOfertas()
        {
            //arrange
            _fixture.CriaDadosFake(true, 200);
            var dal = new OfertaViagemDAL(_context);
            //act
            var ofertas = dal.RecuperarTodas();
            //assert
            Assert.NotEmpty(ofertas);
        }
    }
}
