using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Viaje
{
    public int IdViaje { get; set; }

    public int IdDestinoIda { get; set; }

    public int IdDestinoLlegada { get; set; }

    public TimeOnly HoraSalida { get; set; }

    public TimeOnly HoraLlegada { get; set; }

    public DateOnly FechaViaje { get; set; }

    public decimal PrecioViaje { get; set; }

    public int CantidadPuestos { get; set; }

    public int IdAerolinea { get; set; }

    public string? Imagen { get; set; }

    public virtual Aerolinea IdAerolineaNavigation { get; set; } = null!;

    public virtual Destino IdDestinoIdaNavigation { get; set; } = null!;

    public virtual Destino IdDestinoLlegadaNavigation { get; set; } = null!;

    public virtual ICollection<Informe> Informes { get; set; } = new List<Informe>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
