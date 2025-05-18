using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public int IdBanco { get; set; }

    public string Cuenta { get; set; } = null!;

    public decimal Monto { get; set; }

    public int IdMetodo { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Banco IdBancoNavigation { get; set; } = null!;

    public virtual MetodoPago IdMetodoNavigation { get; set; } = null!;
}
