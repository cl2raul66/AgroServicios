using AgroserviciosTienda.Modelos;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Repositorios;

public interface IEntradasRepositorio
{
    void Insert(Entrada entity);
    IEnumerable<Entrada> GetAll();
    IEnumerable<Entrada> GetByAny(Expression<Func<Entrada, bool>> query);
    Entrada Get(Expression<Func<Entrada, bool>> query);

    bool AnyEntrada { get; }
}
