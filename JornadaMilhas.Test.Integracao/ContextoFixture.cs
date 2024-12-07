using Bogus;
using JornadaMilhas.Dados;
using JornadaMilhas.Test.Integracao.Fakers;
using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace JornadaMilhas.Test.Integracao
{
    public class ContextoFixture : IAsyncLifetime
    {
        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
                                                          .WithImage("mcr.microsoft.com/mssql/server:2022-latest").Build();

        internal JornadaMilhasContext Context;

        public List<OfertaViagem> CriaDadosFake(bool salvar, int quantidade)
        {
            var ofertas = new OfertaViagemDataBuilder().Build(quantidade);

            if (salvar)
            {
                Context.OfertasViagem.AddRange(ofertas);
                Context.SaveChanges();
            }

            return ofertas;
        }

        public async Task LimpaDadosDoBanco()
        {
            var querry = $@"DELETE FROM OfertasViagem
                            DELETE FROM Rotas";

            await Context.Database.ExecuteSqlRawAsync(querry);
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _msSqlContainer.StartAsync();
                var options = new DbContextOptionsBuilder<JornadaMilhasContext>()
               .UseSqlServer(_msSqlContainer.GetConnectionString())
               .Options;
                Context = new JornadaMilhasContext(options);
                Context.Database.Migrate();
            }
            catch (Exception e)
            {
                throw new NotImplementedException(e.Message);
            }
        }

        public async Task DisposeAsync()
        {
            await _msSqlContainer.DisposeAsync();
        }

        //private void ObterContext()
        //{
        //    //var builder = new ConfigurationBuilder()
        //    //            .SetBasePath(Directory.GetCurrentDirectory())
        //    //            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //    //IConfigurationRoot configuration = builder.Build();
        //    //string connectionString = configuration.GetSection("AppSettings")["Principal"];

        //    //var options = new DbContextOptionsBuilder<JornadaMilhasContext>()
        //    //    .UseSqlServer(connectionString)
        //    //    .Options;



        //    //Context = new JornadaMilhasContext(options);
        //}
    }
}
