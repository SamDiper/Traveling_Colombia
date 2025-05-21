using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Destino
{
    public int IdDestino { get; set; }

    public string NombreDestino { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public virtual ICollection<Plane> Planes { get; set; } = new List<Plane>();

    public virtual ICollection<Viaje> ViajeIdDestinoIdaNavigations { get; set; } = new List<Viaje>();

    public virtual ICollection<Viaje> ViajeIdDestinoLlegadaNavigations { get; set; } = new List<Viaje>();

    public virtual ICollection<VuelosAerolinea> VuelosAerolineas { get; set; } = new List<VuelosAerolinea>();
}
