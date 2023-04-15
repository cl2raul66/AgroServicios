using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Servicios;

public class EntradasLiteDbServicio : IEntradasRepositorio
{
    readonly ILiteCollection<Entrada> collection;

    public EntradasLiteDbServicio()
    {
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.CacheDirectory, "Entradas.db")
        };

        var db = new LiteDatabase(cnx);
        collection = db.GetCollection<Entrada>();
    }

    public bool AnyEntrada() => collection.Count() > 0;

    public Entrada Get(Expression<Func<Entrada, bool>> query) => collection.FindOne(query);

    public IEnumerable<Entrada> GetAll() => collection.FindAll();

    public IEnumerable<Entrada> GetByAny(Expression<Func<Entrada, bool>> query) => collection.Find(query);

    public void Insert(Entrada entity) => collection.Insert(entity);
}
