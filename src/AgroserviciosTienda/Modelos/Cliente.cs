namespace AgroserviciosTienda.Modelos;

public class Cliente : Contacto
{
    public Cliente(string nombre, string nit, string telefono, string email, string direccion, bool esempresa)
    {
        Nombre = nombre;
        Nit = nit;
        Telefono = telefono;
        EMail = email;
        Direccion = direccion;
        EsEmpresa = esempresa;
        EsCliente = true;
    }
}
