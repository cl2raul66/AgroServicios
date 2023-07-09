namespace AgroserviciosTienda.Modelos;

public record TipoUnidad
{
    public string Nombre { get; init; }
    public string Abreviatura { get; init; }

    public TipoUnidad()
    {
        Nombre = string.Empty;
        Abreviatura = string.Empty;
    }
    public TipoUnidad(string nombre, string abreviatura)
    {
        Nombre = nombre;
        Abreviatura = abreviatura;
    }

    public override string ToString()
    {
        return $"{Nombre} - {Abreviatura}";
    }
}