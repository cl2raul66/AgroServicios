namespace AgroserviciosTienda.Modelos;

public class Contacto
{
    public string Nombre { get; set; }
    public string Nit { get; set; }
    public string Telefono { get; set; }
    public string EMail { get; set; }
    public string Direccion { get; set; }
    public bool EsEmpresa { get; set; }
    public bool EsProbeedor { get; set; } = false;
    public bool EsCliente { get; set; } = false;
}
