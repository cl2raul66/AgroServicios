using AgroserviciosTienda.Modelos;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Repositorios;

public interface IProveedoresRepositorio
{
    void Insert(Contacto entity);
    IEnumerable<Contacto> GetAll();
    IEnumerable<Contacto> GetByAny(Expression<Func<Contacto, bool>> query);
    Contacto Get(Expression<Func<Contacto, bool>> query);

    bool AnyContacto();
}
