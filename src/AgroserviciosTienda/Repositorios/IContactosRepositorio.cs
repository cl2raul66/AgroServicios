using AgroserviciosTienda.Modelos;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Repositorios;

public interface IContactosRepositorio<T> where T : class
{
    bool AnyContacto();
    T Get(Expression<Func<Contacto, bool>> query);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByAny(Expression<Func<Contacto, bool>> query);
    void Insert(Contacto entity);
}
