using LiteDB;

namespace AgroserviciosTienda.Modelos;

public class Cliente
{
    [BsonId]
    public string Nombre { get; set; }
    public string Nit { get; set; }
    public string Telefono { get; set; }
    public string EMail { get; set; }
    public string Direccion { get; set; }
    public bool EsEmpresa { get; set; }

    public Cliente() { }

    public Cliente(string nombre, string nit, string telefono, string email, string direccion, bool esempresa)
    {
        Nombre = nombre; Nit = nit; Telefono = telefono; EMail = email; Direccion = direccion; EsEmpresa = esempresa;
    }

    public override string ToString()
    {
        return Nombre;
    }
}
