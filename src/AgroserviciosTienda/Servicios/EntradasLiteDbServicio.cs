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
        var mapper = BsonMapper.Global;
        mapper.SerializeNullValues = true;

        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "Entradas.db")
        };

        var db = new LiteDatabase(cnx, mapper);
        collection = db.GetCollection<Entrada>();
    }

    public bool AnyEntrada => collection.Count() > 0;

    public Entrada Get(Expression<Func<Entrada, bool>> query) => collection.FindOne(query);

    public IEnumerable<Entrada> GetAll => collection.FindAll();

    public IEnumerable<Entrada> GetByAny(Expression<Func<Entrada, bool>> query) => collection.Find(query);

    public void Insert(Entrada entity) => collection.Insert(entity);

    public ProductoEntrada GetProductoentrada(Producto entity)
    {
        return collection
            .Include(x => x.Productos)
            .FindAll().SelectMany(x => x.Productos)
            .LastOrDefault(x => x.ToString() == entity.ToString());
    }
}
