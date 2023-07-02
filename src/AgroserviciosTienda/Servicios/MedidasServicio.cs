using System.Globalization;
using System.Text.Json;
using UnitsNet;

namespace AgroserviciosTienda.Servicios;

public interface IMedidasServicio
{
    IEnumerable<string> MagnitudAll { get; }
    IEnumerable<string> MagnitudesUso { get; }

    IEnumerable<string> EliminarMagnitudUso(string magnitudUso);
    IEnumerable<string> InsertarMagnitudUso(string magnitudUso);
    IEnumerable<string> Unidades(string magnitud);
    IEnumerable<string> UnidadesNombre(string magnitud);
    IEnumerable<string> UnidadesSimbolo(string magnitud);
    IEnumerable<string> UnidadesUso(string magnitudUso);
}

public class MedidasServicio : IMedidasServicio
{
    readonly Dictionary<string, string> areaUnidades = new()
    {
        {"Acre","Acre"},
        {"Hectare","Hectárea"},
        {"SquareCentimeter","Centímetro cuadrado"},
        {"SquareMeter","Metro cuadrado"},
        {"SquareMillimeter","Milímetro cuadrado"},
        {"SquareFoot","Pie cuadrado"},
        {"SquareInch","Pulgada cuadrada"},
        {"SquareYard","Yarda cuadrada"}
    };

    readonly Dictionary<string, string> masaUnidades = new()
    {
        {"Gram","Gramo"},
        {"Milligram","Miligramo"},
        {"Kilogram","Kilogramo"},
        {"Pound","Libra"},
        {"Ounce","Onza"}
    };

    readonly Dictionary<string, string> longitudUnidades = new()
    {
        {"Centimeter","Centímetro"},
        {"Meter","Metro"},
        {"Millimeter","Milímetro"},
        {"Foot","Pie"},
        {"Inch","Pulgada"},
        {"Yard","Yarda"}
    };

    readonly Dictionary<string, string> volumenUnidades = new()
    {
        {"Milliliter","Mililitro"},
        {"Liter","Litro"},
        {"CubicMeter","Metro cúbico"},
        {"CubicCentimeter","Centímetro cúbico"},
        {"CubicFoot","Pie cúbico"},
        {"CubicInch","Pulgada cúbica"},
        {"UsGallon","Galón"},
        {"UsOunce","Onza"}
    };

    readonly CultureInfo culture = new("es-ES");

    public MedidasServicio()
    {
        if (!Preferences.ContainsKey("MagnitudesUso"))
        {
            Preferences.Set("MagnitudesUso", JsonSerializer.Serialize(MagnitudAll));
        }
    }

    public IEnumerable<string> MagnitudAll => new List<string> { "Area", "Masa", "Longitud", "Volumen" };

    public IEnumerable<string> Unidades(string magnitud) => magnitud switch
    {
        "Area" => Area.Units.Select(x => $"{Area.GetAbbreviation(x, culture)} ({x})"),
        "Masa" => Mass.Units.Select(x => $"{Mass.GetAbbreviation(x, culture)} ({x})"),
        "Longitud" => Length.Units.Select(x => $"{Length.GetAbbreviation(x, culture)} ({x})"),
        "Volumen" => Volume.Units.Select(x => $"{Volume.GetAbbreviation(x, culture)} ({x})"),
        _ => Array.Empty<string>()
    };

    public IEnumerable<string> UnidadesNombre(string magnitud) => magnitud switch
    {
        "Area" => areaUnidades.Values,
        "Masa" => masaUnidades.Values,
        "Longitud" => longitudUnidades.Values,
        "Volumen" => volumenUnidades.Values,
        _ => Array.Empty<string>()
    };

    public IEnumerable<string> UnidadesSimbolo(string magnitud) => magnitud switch
    {
        "Area" => Area.Units.Select(x => Area.GetAbbreviation(x, culture)),
        "Masa" => Mass.Units.Select(x => Mass.GetAbbreviation(x, culture)),
        "Longitud" => Length.Units.Select(x => Length.GetAbbreviation(x, culture)),
        "Volumen" => Volume.Units.Select(x => Volume.GetAbbreviation(x, culture)),
        _ => Array.Empty<string>()
    };

    public IEnumerable<string> MagnitudesUso => JsonSerializer.Deserialize<IEnumerable<string>>(Preferences.Get(nameof(MagnitudesUso), string.Empty));

    public IEnumerable<string> UnidadesUso(string magnitudUso) => magnitudUso switch
    {
        "Area" => JsonSerializer.Deserialize<IEnumerable<string>>(Preferences.Get("AreaUnidades", string.Empty)),
        "Masa" => JsonSerializer.Deserialize<IEnumerable<string>>(Preferences.Get("MasaaUnidades", string.Empty)),
        "Longitud" => JsonSerializer.Deserialize<IEnumerable<string>>(Preferences.Get("LongitudUnidades", string.Empty)),
        "Volumen" => JsonSerializer.Deserialize<IEnumerable<string>>(Preferences.Get("VolumenUnidades", string.Empty)),
        _ => Array.Empty<string>()
    };

    public IEnumerable<string> InsertarMagnitudUso(string magnitudUso)
    {
        var m = MagnitudesUso.Append(magnitudUso);

        Preferences.Set(nameof(MagnitudesUso), JsonSerializer.Serialize(m));

        return MagnitudesUso;
    }

    public IEnumerable<string> EliminarMagnitudUso(string magnitudUso)
    {
        var m = MagnitudesUso.Except(new[] { magnitudUso });

        Preferences.Set(nameof(MagnitudesUso), JsonSerializer.Serialize(m));

        return MagnitudesUso;
    }
    //public void Insert(string magnitudUso, string)

    #region Extra

    #endregion
}
