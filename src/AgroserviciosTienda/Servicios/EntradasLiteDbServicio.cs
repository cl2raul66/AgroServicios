using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public IEnumerable<Entrada> GetAll() => collection.FindAll();

    public IEnumerable<Entrada> GetByAny(IQueryable query)
    {
        throw new NotImplementedException();
    }

    public void Insert(Entrada entity) => collection.Insert(entity);
}
