using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public DateOnly FechaReserva { get; set; }

    public int? IdViaje { get; set; }

    public int IdEstadoReserva { get; set; }

    public int IdUsuario { get; set; }

    public int? IdPlan { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Estado IdEstadoReservaNavigation { get; set; } = null!;

    public virtual Plane? IdPlanNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Viaje? IdViajeNavigation { get; set; }

    public virtual ICollection<Informe> Informes { get; set; } = new List<Informe>();
}
