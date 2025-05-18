using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Destino
{
    public int IdDestino { get; set; }

    public string NombreDestino { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public virtual ICollection<Plane> PlaneIdDestinoIdaNavigations { get; set; } = new List<Plane>();

    public virtual ICollection<Plane> PlaneIdDestinoLlegadaNavigations { get; set; } = new List<Plane>();

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();

    public virtual ICollection<VuelosAerolinea> VuelosAerolineas { get; set; } = new List<VuelosAerolinea>();
}
