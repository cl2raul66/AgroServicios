namespace AgroserviciosTienda.Modelos;

public class Empaque
{
    public string Medida { get; set; }
    public string Unidad { get; set; }
    public double Valor { get; set; }

    public Empaque() { }

    public Empaque(string medida, string unidad, double valor)
    {
        Medida = medida;
        Unidad = unidad;
        Valor = valor;
    }
}
