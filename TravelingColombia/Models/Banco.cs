using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Banco
{
    public int IdBanco { get; set; }

    public string NombreBanco { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
