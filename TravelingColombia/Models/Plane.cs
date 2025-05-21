using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Plane
{
    public int IdPlan { get; set; }

    public int IdDestinoIda { get; set; }

    public string NombrePlan { get; set; } = null!;

    public DateOnly FechaIda { get; set; }

    public DateOnly FechaRegreso { get; set; }

    public int IdTipoPlan { get; set; }

    public string Descripcion { get; set; } = null!;

    public int CantidadPersonas { get; set; }

    public string Imagen { get; set; } = null!;

    public decimal PrecioPlan { get; set; }

    public int IdHotel { get; set; }

    public int IdAerolinea { get; set; }

    public virtual Aerolinea IdAerolineaNavigation { get; set; } = null!;

    public virtual Destino IdDestinoIdaNavigation { get; set; } = null!;

    public virtual Hotele IdHotelNavigation { get; set; } = null!;

    public virtual TipoPlan IdTipoPlanNavigation { get; set; } = null!;

    public virtual ICollection<Informe> Informes { get; set; } = new List<Informe>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
