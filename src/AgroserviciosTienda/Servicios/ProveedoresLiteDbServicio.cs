using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Servicios;

public class ProveedoresLiteDbServicio : IContactosRepositorio<Proveedor>
{
    readonly ILiteCollection<Contacto> collection;

    public ProveedoresLiteDbServicio()
    {
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "Proveedores.db")
        };

        var db = new LiteDatabase(cnx);
        collection = db.GetCollection<Contacto>();
    }

    public bool AnyContacto() => collection.Count() > 0;

    public Proveedor Get(Expression<Func<Contacto, bool>> query) => collection.FindOne(query) as Proveedor;

    public IEnumerable<Proveedor> GetAll() => collection.FindAll().OfType<Proveedor>();

    public IEnumerable<Proveedor> GetByAny(Expression<Func<Contacto, bool>> query) => collection.Find(query).OfType<Proveedor>();

    public void Insert(Contacto entity) => collection.Insert(entity);
}
