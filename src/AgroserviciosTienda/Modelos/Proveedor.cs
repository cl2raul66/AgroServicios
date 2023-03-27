namespace AgroserviciosTienda.Modelos;

public class Proveedor : Contacto
{
    public Proveedor(string nombre, string nit, string telefono, string email, string direccion)
    {
        Nombre = nombre;
        Nit = nit;
        Telefono = telefono;
        EMail = email;
        Direccion = direccion;
    }
}
