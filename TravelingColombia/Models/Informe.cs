using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Informe
{
    public int IdInforme { get; set; }

    public int? IdPlan { get; set; }

    public int? IdViaje { get; set; }

    public int? IdReserva { get; set; }

    public int? IdFactura { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }

    public virtual Plane? IdPlanNavigation { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }

    public virtual Viaje? IdViajeNavigation { get; set; }
}
