using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.Servicios;

public class CacheAgregarentradaServicio
{
    public CacheAgregarentradaServicio()
    {
Preferences.Default.Set("CacheAgregarentrada", "John");
    }

}
