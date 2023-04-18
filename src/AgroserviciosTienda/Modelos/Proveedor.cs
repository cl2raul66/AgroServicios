namespace AgroserviciosTienda.Modelos;

public class Proveedor : Contacto
{
    public Proveedor(string nombre, string nit, string telefono, string email, string direccion, bool esempresa)
    {
        Nombre = nombre;
        Nit = nit;
        Telefono = telefono;
        EMail = email;
        Direccion = direccion;
        EsEmpresa = esempresa;
        EsProbeedor = true;
    }
}
