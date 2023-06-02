using System.Linq.Expressions;

namespace AgroserviciosTienda.Repositorios;

public interface IContactosRepositorio<T> where T : class
{
    bool AnyContacto();
    T Get(Expression<Func<T, bool>> query);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByAny(Expression<Func<T, bool>> query);
    void Insert(T entity);
}
