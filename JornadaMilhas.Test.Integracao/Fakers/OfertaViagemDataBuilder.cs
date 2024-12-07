using Bogus;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integracao.Fakers
{
    internal class OfertaViagemDataBuilder:Faker<OfertaViagem>
    {
        public Rota? Rota { get; set; }
        public Periodo? Periodo { get; set; }
        public double? Valor { get; set; }
        public double? Desconto { get; set; }

        public OfertaViagemDataBuilder()
        {
            CustomInstantiator(factoryMethod: f =>
            {
                var rota = Rota ?? new RotaDataBuilder().Build();
                var periodo = Periodo ?? new PeriodoDataBuilder().Build();
                var valor = Valor ?? f.Random.Double(100, 1000);
                var desconto = Desconto ?? f.Random.Double(0, 20);
                return new OfertaViagem(rota, periodo, valor)
                {
                    Desconto = desconto
                };
            });
        }
        public OfertaViagem Build() => Generate();
        public List<OfertaViagem> Build(int qntdd) => Generate(qntdd);
    }
}
