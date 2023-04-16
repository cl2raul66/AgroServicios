namespace AgroserviciosTienda.Modelos;

//public record Empaque
//(
//    string Medida,
//    string Unidad,
//    double Valor
//);

public class Empaque
{
    public string Medida { get; }
    public string Unidad { get; }
    public double Valor { get; }

    public Empaque(string medida, string unidad, double valor)
    {
        Medida = medida;
        Unidad = unidad;
        Valor = valor;
    }
}
