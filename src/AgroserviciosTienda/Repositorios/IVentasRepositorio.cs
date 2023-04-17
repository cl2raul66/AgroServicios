using AgroserviciosTienda.Modelos;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Repositorios;

public interface IVentasRepositorio
{
    void Insert(Venta entity);
    IEnumerable<Venta> GetAll { get; }
    IEnumerable<Venta> GetByAny(Expression<Func<Venta, bool>> query);
    Venta Get(Expression<Func<Venta, bool>> query);

    bool AnyVenta { get; }
}
