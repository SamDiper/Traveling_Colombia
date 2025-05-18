using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Hotele
{
    public int IdHotel { get; set; }

    public string UbicacionHotel { get; set; } = null!;

    public string NombreHotel { get; set; } = null!;

    public virtual ICollection<Habitacione> Habitaciones { get; set; } = new List<Habitacione>();

    public virtual ICollection<Plane> Planes { get; set; } = new List<Plane>();
}
