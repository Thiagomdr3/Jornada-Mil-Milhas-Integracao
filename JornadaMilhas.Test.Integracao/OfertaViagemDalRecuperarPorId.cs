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
    public class OfertaViagemDalRecuperarPorId
    {
        private readonly JornadaMilhasContext _context;
        private readonly ContextoFixture _fixture;
        public OfertaViagemDalRecuperarPorId(ITestOutputHelper output, ContextoFixture contextoFixture)
        {
            _context = contextoFixture.Context;
            output.WriteLine(_context.GetHashCode().ToString());
            _fixture = contextoFixture;
        }
        public void Dispose()
        {
            _fixture.LimpaDadosDoBanco();
        }


        [Fact]
        public void RetornaNullQuandoIdInexistente()
        {
            //arrange
            var ofertas = _fixture.CriaDadosFake(true,200);
            var dal = new OfertaViagemDAL(_context);

            //act
            var ofertaRecuperada = dal.RecuperarPorId(0);
            
            //assert
            Assert.Null(ofertaRecuperada);
        }
    }
}
