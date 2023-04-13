using AgroserviciosTienda.Modelos;

namespace AgroserviciosTienda.Repositorios;

public interface IEntradasRepositorio
{
    void Insert(Entrada entity);    
    IEnumerable<Entrada> GetAll();
    IEnumerable<Entrada> GetByAny(IQueryable query);

    bool AnyEntrada();
}
