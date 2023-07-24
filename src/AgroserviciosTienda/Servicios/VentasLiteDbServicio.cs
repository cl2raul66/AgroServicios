using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;
using System.Linq.Expressions;

namespace AgroserviciosTienda.Servicios;

public class VentasLiteDbServicio : IVentasRepositorio
{
    readonly ILiteCollection<Venta> collection;

    public VentasLiteDbServicio()
    {
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "Ventas.db")
        };

        var db = new LiteDatabase(cnx);
        collection = db.GetCollection<Venta>();
    }

    public IEnumerable<Venta> GetAll => collection.FindAll();

    public bool AnyVenta => collection.Count() > 0;

    public Venta Get(Expression<Func<Venta, bool>> query) => collection.FindOne(query);

    public IEnumerable<Venta> GetByAny(Expression<Func<Venta, bool>> query) => collection.Find(query);

    public void Insert(Venta entity) => collection.Insert(entity);
}
