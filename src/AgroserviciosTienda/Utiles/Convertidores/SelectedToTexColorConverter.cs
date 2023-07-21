using Java.Util;
using System.Globalization;

namespace AgroserviciosTienda.Utiles.Convertidores;

public class SelectedToTexColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isSelected = (bool)value;

        return isSelected 
            ? GetColorFromResources("Primary") 
            : GetColorFromResources("Gray500");        
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    #region Extra
    Color GetColorFromResources(string key)
    {
        var d = Application.Current.Resources.MergedDictionaries;
        foreach (var item in d)
        {
            if (item.TryGetValue(key, out object c) && c is Color color)
            {
                return color;
            }
        }
        return Colors.White;
    }
    #endregion
}
