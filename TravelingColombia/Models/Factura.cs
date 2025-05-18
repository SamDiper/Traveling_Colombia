using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly FechaPago { get; set; }

    public int IdReserva { get; set; }

    public int IdPago { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Descuento { get; set; }

    public decimal Total { get; set; }

    public virtual Pago IdPagoNavigation { get; set; } = null!;

    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    public virtual ICollection<Informe> Informes { get; set; } = new List<Informe>();
}
