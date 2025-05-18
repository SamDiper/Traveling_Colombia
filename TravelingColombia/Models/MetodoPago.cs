using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class MetodoPago
{
    public int IdMetodo { get; set; }

    public string MetodoPago1 { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
