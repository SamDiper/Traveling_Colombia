using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Clase
{
    public int IdClases { get; set; }

    public string NombreClase { get; set; } = null!;

    public decimal PrecioClase { get; set; }

    public virtual ICollection<Plane> Planes { get; set; } = new List<Plane>();

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
