using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;

namespace AgroserviciosTienda.Servicios;

public class InventarioLiteDbServicio : IInventarioRepositorio
{
    readonly ILiteCollection<Inventario> collection;

    public InventarioLiteDbServicio()
    {
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "Inventario.db")
        };
        var db = new LiteDatabase(cnx);
        collection = db.GetCollection<Inventario>();
    }

    public IEnumerable<Inventario> GetAll => collection.FindAll();

    public bool AnyInventario => collection.Count() > 0;

    public void Delete(Inventario entity)
    {
        collection.Delete(entity.Id);
    }

    public void Upset(Inventario entity)
    {
        var any = GetAll.FirstOrDefault(x => x.ToString() == entity.ToString());
        Inventario newInventario = any is null
            ? new(entity.Articulo, entity.Existencia, ObjectId.NewObjectId().ToString())
            : (entity.PrecioInicial == 0
            ? new(entity.Articulo, entity.Existencia + any.Existencia, any.Id)
            : entity);
        var r = collection.Upsert(newInventario.Id, newInventario);
    }
}
