using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Servicios;

public class ClientesLiteDbServicio : IContactosRepositorio<Cliente>
{
    readonly ILiteCollection<Cliente> collection;

    public ClientesLiteDbServicio()
    {
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "Clientes.db")
        };

        LiteDatabase db = new(cnx);
        collection = db.GetCollection<Cliente>();
    }

    public bool AnyContacto => collection.Count() > 0;

    public Cliente Get(Expression<Func<Cliente, bool>> query) => collection.FindOne(query);

    public IEnumerable<Cliente> GetAll => collection.FindAll().OfType<Cliente>();

    public IEnumerable<Cliente> GetByAny(Expression<Func<Cliente, bool>> query) => collection.Find(query).OfType<Cliente>();

    public void Insert(Cliente entity) => collection.Insert(entity);

    public void Delete(string nombre) => collection.Delete(nombre);
}
