using LiteDB;

namespace AgroserviciosTienda.Modelos;

public record Magnitud
{
    [BsonId]
    public string Nombre { get; init; }
    public TipoUnidad[] Unidades { get; init; }
    public Magnitud()
    {
        Nombre = string.Empty;
        Unidades = Array.Empty<TipoUnidad>();
    }
    public Magnitud(string nombre, TipoUnidad[] unidades)
    {
        Nombre = nombre;
        Unidades = unidades;
    }
    public Magnitud(string nombre, IEnumerable<TipoUnidad> unidades)
    {
        Nombre = nombre;
        Unidades = unidades.ToArray();
    }
}
