using AgroserviciosTienda.Modelos;

namespace AgroserviciosTienda.Repositorios;

public interface IInventarioRepositorio
{
    void Upset(Inventario entity);
    void Delete(Inventario entity);
    IEnumerable<Inventario> GetAll { get; }
    bool AnyInventario { get; }
}
