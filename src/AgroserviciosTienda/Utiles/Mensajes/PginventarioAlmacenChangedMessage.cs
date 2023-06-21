using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AgroserviciosTienda.Utiles.Mensajes;

public class PginventarioAlmacenChangedMessage : ValueChangedMessage<IEnumerable<ProductoEntrada>>
{
    public PginventarioAlmacenChangedMessage(IEnumerable<ProductoEntrada> newEntradas) : base(newEntradas)
    {
        
    }
}
