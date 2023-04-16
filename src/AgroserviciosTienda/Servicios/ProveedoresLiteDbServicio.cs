using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Servicios;

public class ProveedoresLiteDbServicio : IProveedoresRepositorio
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

    public Contacto Get(Expression<Func<Contacto, bool>> query) => collection.FindOne(query);

    public IEnumerable<Contacto> GetAll() => collection.FindAll();

    public IEnumerable<Contacto> GetByAny(Expression<Func<Contacto, bool>> query) => collection.Find(query);

    public void Insert(Contacto entity) => collection.Insert(entity);
}
