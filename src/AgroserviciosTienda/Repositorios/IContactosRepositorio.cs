using System.Linq.Expressions;

namespace AgroserviciosTienda.Repositorios;

public interface IContactosRepositorio<T> where T : class
{
    bool AnyContacto { get; }
    T Get(Expression<Func<T, bool>> query);
    IEnumerable<T> GetAll { get; }
    IEnumerable<T> GetByAny(Expression<Func<T, bool>> query);
    void Insert(T entity);
    void Delete(string nombre);
}
