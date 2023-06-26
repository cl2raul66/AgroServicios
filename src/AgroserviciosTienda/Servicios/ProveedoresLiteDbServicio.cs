using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Servicios;

public class ProveedoresLiteDbServicio : IContactosRepositorio<Proveedor>
{
    readonly ILiteCollection<Proveedor> collection;
    LiteDatabase db;

    public ProveedoresLiteDbServicio()
    {
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "Proveedores.db")
        };

        db = new(cnx);
        collection = db.GetCollection<Proveedor>();
    }

    public bool AnyContacto => collection.Count() > 0;

    public Proveedor Get(Expression<Func<Proveedor, bool>> query) => collection.FindOne(query);

    public IEnumerable<Proveedor> GetAll => collection.FindAll().OfType<Proveedor>();

    public IEnumerable<Proveedor> GetByAny(Expression<Func<Proveedor, bool>> query) => collection.Find(query).OfType<Proveedor>();

    public void Insert(Proveedor entity) => collection.Insert(entity.Nombre, entity);

    public void Delete(string nombre) => collection.Delete(new BsonValue(nombre));
}
