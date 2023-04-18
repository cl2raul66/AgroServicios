using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Servicios;

public class ClientesLiteDbServicio : IContactosRepositorio<Cliente>
{
    readonly ILiteCollection<Contacto> collection;

    public ClientesLiteDbServicio()
    {
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "Clientes.db")
        };

        var db = new LiteDatabase(cnx);
        collection = db.GetCollection<Contacto>();
    }

    public bool AnyContacto() => collection.Count() > 0;

    public Cliente Get(Expression<Func<Contacto, bool>> query) => collection.FindOne(query) as Cliente;

    public IEnumerable<Cliente> GetAll() => collection.FindAll().OfType<Cliente>();

    public IEnumerable<Cliente> GetByAny(Expression<Func<Contacto, bool>> query) => collection.Find(query).OfType<Cliente>();

    public void Insert(Contacto entity) => collection.Insert(entity);
}
