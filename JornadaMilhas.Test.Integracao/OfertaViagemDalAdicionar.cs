using Bogus;
using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;
using JornadaMilhas.Test.Integracao.Fakers;

namespace JornadaMilhas.Test.Integracao;

[Collection(nameof(ContextCollection))]
public class OfertaViagemDalAdicionar
{
    private List<string> estadosBrasileiros;
    private JornadaMilhasContext _context;

    public OfertaViagemDalAdicionar(ITestOutputHelper output, ContextoFixture fixture)
    {
        _context = fixture.Context;
        //estadosBrasileiros = fixture.EstadosBrasileiros;
        estadosBrasileiros = new RotaDataBuilder().Estados;
        output.WriteLine(_context.GetHashCode().ToString());
    }

    [Fact]
    public void RegistraOfertaNoBando()
    {
        //arrange
        var fakePeriod = new Faker<Periodo>()
            .CustomInstantiator(f =>
            {
                DateTime dataInicial = f.Date.Soon();
                return new Periodo(dataInicial, dataInicial.AddDays(30));
            });

        var fakeRota = new Faker<Rota>()
            .CustomInstantiator(f =>
            {
                var origem = f.PickRandom(estadosBrasileiros);
                var destino = f.PickRandom(estadosBrasileiros.Where(e => e != origem));
                return new Rota(origem, destino);
            });

        var fakeOferta = new Faker<OfertaViagem>()
            .CustomInstantiator(f =>
            {
                var rota = fakeRota.Generate();
                var periodo = fakePeriod.Generate();
                return new OfertaViagem(rota, periodo, 100 * f.Random.Int(1, 100));
            });

        //var options = new DbContextOptionsBuilder<JornadaMilhasContext>()
        //    .UseInMemoryDatabase(databaseName: "JornadaMilhas")
        //    .Options;
        


        var oferta = fakeOferta.Generate();
        var dal = new OfertaViagemDAL(_context);


        dal.Adicionar(oferta);

        Assert.NotNull(dal.RecuperarPorId(oferta.Id));
        Assert.Equal(oferta, dal.RecuperarPorId(oferta.Id));

    }
}