using Bogus;
using JornadaMilhas.Dados;
using JornadaMilhas.Test.Integracao.Fakers;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integracao
{
    [Collection(nameof(ContextCollection))]
    public class OfertaViagemDalRecuperaMaiorDesconto:IDisposable
    {
        private readonly JornadaMilhasContext _context;
        private readonly ContextoFixture _fixture;
        public OfertaViagemDalRecuperaMaiorDesconto(ContextoFixture fixture)
        {
            _context = fixture.Context;
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture.LimpaDadosDoBanco();
        }

        [Fact]
        public void RetornaMaiorDesconto()
        {
            //arrange
            var ofertas = _fixture.CriaDadosFake(true, 200);

            var ofertaMaiorDesconto = new OfertaViagem(new RotaDataBuilder().Build(), new PeriodoDataBuilder().Build(), 100)
            {
                Desconto = 30
            };

            var dal = new OfertaViagemDAL(_context);
            _context.OfertasViagem.Add(ofertaMaiorDesconto);
            _context.SaveChanges();

            //act
            var filtro = new Func<OfertaViagem, bool>(o => o.Ativa = true);

            //assert
            Assert.Equal(ofertaMaiorDesconto, dal.RecuperaMaiorDesconto(filtro));
        }

        [Fact]
        public void RetornaMaiorDesconto2()
        {
            //arrange
            var ofertas = _fixture.CriaDadosFake(true, 200);

            var ofertaMaiorDesconto = new OfertaViagem(new RotaDataBuilder().Build(), new PeriodoDataBuilder().Build(), 100)
            {
                Desconto = 40
            };

            var dal = new OfertaViagemDAL(_context);
            _context.OfertasViagem.Add(ofertaMaiorDesconto);
            _context.SaveChanges();

            //act
            var filtro = new Func<OfertaViagem, bool>(o => o.Ativa = true);

            //assert
            Assert.Equal(ofertaMaiorDesconto, dal.RecuperaMaiorDesconto(filtro));
        }
    }
}
