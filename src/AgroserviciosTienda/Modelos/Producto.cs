namespace AgroserviciosTienda.Modelos;

public class Producto
{
    public string Nombre { get; set; }
    public Empaque Presentacion { get; set; }

    public override string ToString()
    {
        return Presentacion?.Valor > 0
            ? $"{Nombre} {Presentacion.Valor} {Presentacion.Unidad}"
            : Nombre;
    }
}
