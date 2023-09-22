using AgroserviciosTienda.Modelos;
using LiteDB;

namespace AgroserviciosTienda.Servicios;

public interface IMedidasServicio
{
    IEnumerable<string> AllMagnitud { get; }

    bool AgregarMagnitud(string magnitud);
    bool AgregarUnidad(string magnitud, TipoUnidad unidad);
    IEnumerable<TipoUnidad> AllUnidades(string magnitud);
    bool ExisteMagnitud(string magnitud);
    bool QuitarMagnitud(string magnitud);
    bool QuitarUnidad(string magnitud, TipoUnidad unidad);
    bool ExisteUnidad(string magnitud, TipoUnidad unidad);
}

public class MedidasServicio : IMedidasServicio
{
    readonly ILiteCollection<Magnitud> collection;
    readonly IBaseMedidasServicio baseMedidasServ;

    public MedidasServicio(IBaseMedidasServicio baseMedidasServicio)
    {
        baseMedidasServ = baseMedidasServicio;
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "MedidasUso.db")
        };

        LiteDatabase db = new(cnx);
        collection = db.GetCollection<Magnitud>();

        if (collection.Count() == 0)
        {
            collection.InsertBulk(baseMedidasServ.AllMagnitud);
        }
    }

    public IEnumerable<string> AllMagnitud => collection.FindAll().Select(x => x.Nombre);

    public bool ExisteMagnitud(string magnitud) => collection.FindOne(x => x.Nombre == magnitud) is not null;

    public bool AgregarMagnitud(string magnitud) => collection.Insert(new Magnitud(magnitud, baseMedidasServ.AllUnidad(magnitud).ToArray())) is not null;

    public bool QuitarMagnitud(string magnitud) => collection.Delete(magnitud);

    public IEnumerable<TipoUnidad> AllUnidades(string magnitud) => collection.FindOne(x => x.Nombre == magnitud).Unidades;

    public bool AgregarUnidad(string magnitud, TipoUnidad unidad)
    {
        var unidades = collection.FindOne(x => x.Nombre == magnitud).Unidades;
        var newUnidades = unidades.Append(unidad);
        return collection.Upsert(new Magnitud(magnitud, newUnidades));
    }

    public bool QuitarUnidad(string magnitud, TipoUnidad unidad)
    {
        var newUnidades = collection.FindOne(x => x.Nombre == magnitud).Unidades.Except(new[] { unidad });
        return collection.Upsert(new Magnitud(magnitud, newUnidades));
    }

    public bool ExisteUnidad(string magnitud, TipoUnidad unidad) => collection.FindOne(x => x.Nombre == magnitud).Unidades.Any(x => x.Equals(unidad));

     
}
