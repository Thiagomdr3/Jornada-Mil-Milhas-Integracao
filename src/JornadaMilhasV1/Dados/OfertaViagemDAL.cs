using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Dados;
public class OfertaViagemDAL
{
    private readonly JornadaMilhasContext _context;

    public OfertaViagemDAL(JornadaMilhasContext context)
    {
        _context = context;
    }

    public void Adicionar(OfertaViagem oferta)
    {
        _context.OfertasViagem.Add(oferta);
        _context.SaveChanges();
    }

    public void AdicionarLista(List<OfertaViagem> oferta)
    {
        _context.OfertasViagem.AddRange(oferta);
        _context.SaveChanges();
    }

    public OfertaViagem? RecuperarPorId(int id)
    {
        return _context.OfertasViagem.FirstOrDefault(o => o.Id == id);
    }

    public IEnumerable<OfertaViagem>? RecuperarPor(Func<OfertaViagem, bool> predicate) =>
        _context.OfertasViagem.Where(predicate);

    public IEnumerable<OfertaViagem> RecuperarTodas()
    {
        return _context.OfertasViagem.ToList();
    }

    public void Atualizar(OfertaViagem oferta)
    {
        _context.OfertasViagem.Update(oferta);
        _context.SaveChanges();
    }

    public void Remover(OfertaViagem oferta)
    {
        _context.OfertasViagem.Remove(oferta);
        _context.SaveChanges();
    }

    public OfertaViagem? RecuperaMaiorDesconto(Func<OfertaViagem, bool> filtro) =>
        _context.OfertasViagem.Where(filtro).OrderBy(o => o.Preco).FirstOrDefault();
}
