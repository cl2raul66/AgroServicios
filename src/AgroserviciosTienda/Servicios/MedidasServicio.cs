using UnitsNet;

namespace AgroserviciosTienda.Servicios;

public interface IMedidasServicio
{
    IEnumerable<string> TiposMedidas { get; }

    IEnumerable<string> Unidades(string tipo);
}

public class MedidasServicio : IMedidasServicio
{
    readonly List<string> tiposMedidas = new() { "Masa", "Longitud", "Volumen" };

    public IEnumerable<string> TiposMedidas => tiposMedidas;

    public IEnumerable<string> Unidades(string tipo) => tipo switch
    {
        "Masa" => Mass.Units.Select(Mass.GetAbbreviation),
        "Longitud" => Length.Units.Select(Length.GetAbbreviation),
        "Volumen" => Volume.Units.Select(Volume.GetAbbreviation),
        _ => Array.Empty<string>()
    };
}
