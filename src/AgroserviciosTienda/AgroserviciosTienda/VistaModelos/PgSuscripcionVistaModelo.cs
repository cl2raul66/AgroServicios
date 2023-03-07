using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgSuscripcionVistaModelo : ObservableValidator
{
    public PgSuscripcionVistaModelo()
    {
        ValidateAllProperties();
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [EmailAddress]
    private string correo;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [MinLength(8)]
    private string clave;
}
