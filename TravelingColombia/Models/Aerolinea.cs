using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Aerolinea
{
    public int IdAerolinea { get; set; }

    public string NombreAerolinea { get; set; } = null!;

    public virtual ICollection<Plane> Planes { get; set; } = new List<Plane>();

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();

    public virtual ICollection<VuelosAerolinea> VuelosAerolineas { get; set; } = new List<VuelosAerolinea>();
}
