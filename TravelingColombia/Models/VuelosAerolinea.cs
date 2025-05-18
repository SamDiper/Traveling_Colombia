using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class VuelosAerolinea
{
    public int IdVueloAerolinea { get; set; }

    public int IdDestino { get; set; }

    public int IdAerolinea { get; set; }

    public decimal Precio { get; set; }

    public virtual Aerolinea IdAerolineaNavigation { get; set; } = null!;

    public virtual Destino IdDestinoNavigation { get; set; } = null!;
}
