using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class TipoPlan
{
    public int IdTipoPlan { get; set; }

    public string NombrePlan { get; set; } = null!;

    public virtual ICollection<Plane> Planes { get; set; } = new List<Plane>();
}
