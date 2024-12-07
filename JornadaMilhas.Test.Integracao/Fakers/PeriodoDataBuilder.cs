using Bogus;
using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test.Integracao.Fakers
{
    internal class PeriodoDataBuilder:Faker<Periodo>
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public PeriodoDataBuilder()
        {
            CustomInstantiator(factoryMethod: f =>
            {
                var dataInicio = DataInicio ?? f.Date.Soon();
                var dataFim = DataFim ?? dataInicio.AddDays(30);
                return new Periodo(dataInicio, dataFim);
            });
        }

        public Periodo Build() => Generate();
    }
}
