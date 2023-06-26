using AgroserviciosTienda.Servicios;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgMedidasDetallesVistaModelo : ObservableObject
{
    readonly IMedidasServicio medidasServ;

    public PgMedidasDetallesVistaModelo(IMedidasServicio medidasServicio)
    {
        medidasServ = medidasServicio;
    }


}
