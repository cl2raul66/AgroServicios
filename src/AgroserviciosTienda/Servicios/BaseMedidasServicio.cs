using AgroserviciosTienda.Modelos;
using LiteDB;
using System.Globalization;
using UnitsNet.Units;
using UnitsNet;

namespace AgroserviciosTienda.Servicios;

public interface IBaseMedidasServicio
{
    IEnumerable<Magnitud> AllMagnitud { get; }
    IEnumerable<string> AllNombresMagnitud { get; }
    bool ExisteMagnitud(string magnitud);
    IEnumerable<TipoUnidad> AllUnidad(string magnitud);
}

public class BaseMedidasServicio : IBaseMedidasServicio
{
    readonly ILiteCollection<Magnitud> collection;
    readonly CultureInfo culture = new("es-ES");

    public BaseMedidasServicio()
    {
        var cnx = new ConnectionString()
        {
            Filename = Path.Combine(FileSystem.Current.AppDataDirectory, "Medidas.db")
        };

        LiteDatabase db = new(cnx);
        collection = db.GetCollection<Magnitud>();

        if (collection.Count() == 0)
        {
            Dictionary<string, string> areaUnidades = new()
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

            Dictionary<string, string> masaUnidades = new()
            {
                {"Gram","Gramo"},
                {"Milligram","Miligramo"},
                {"Kilogram","Kilogramo"},
                {"Pound","Libra"},
                {"Ounce","Onza"}
            };

            Dictionary<string, string> longitudUnidades = new()
            {
                {"Centimeter","Centímetro"},
                {"Meter","Metro"},
                {"Millimeter","Milímetro"},
                {"Foot","Pie"},
                {"Inch","Pulgada"},
                {"Yard","Yarda"}
            };

            Dictionary<string, string> volumenUnidades = new()
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
            var _areaUnidades = areaUnidades.Select(x => new TipoUnidad(x.Value, Area.GetAbbreviation(Enum.Parse<AreaUnit>(x.Key), culture))).ToArray();
            collection.Insert(new Magnitud("Area", _areaUnidades));
            var _masaUnidades = masaUnidades.Select(x => new TipoUnidad(x.Value, Mass.GetAbbreviation(Enum.Parse<MassUnit>(x.Key), culture))).ToArray();
            collection.Insert(new Magnitud("Masa", _masaUnidades));
            var _longitudUnidades = longitudUnidades.Select(x => new TipoUnidad(x.Value, Length.GetAbbreviation(Enum.Parse<LengthUnit>(x.Key), culture))).ToArray();
            collection.Insert(new Magnitud("Longitud", _longitudUnidades));
            var _volumenUnidades = volumenUnidades.Select(x => new TipoUnidad(x.Value, Volume.GetAbbreviation(Enum.Parse<VolumeUnit>(x.Key), culture))).ToArray();
            collection.Insert(new Magnitud("Volumen", _volumenUnidades));
        }
    }

    public bool ExisteMagnitud(string magnitud) => collection.FindOne(x => x.Nombre == magnitud) is not null;

    public IEnumerable<string> AllNombresMagnitud => collection.FindAll().Select(x => x.Nombre);

    public IEnumerable<Magnitud> AllMagnitud => collection.FindAll();

    public IEnumerable<TipoUnidad> AllUnidad(string magnitud) => collection.FindOne(x => x.Nombre == magnitud).Unidades;
}
