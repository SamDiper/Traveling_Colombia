using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Habitacione
{
    public int IdHabitacion { get; set; }

    public string TipoHabitacion { get; set; } = null!;

    public decimal Precio { get; set; }

    public int CantidadHabitacion { get; set; }

    public int IdHotel { get; set; }

    public virtual Hotele IdHotelNavigation { get; set; } = null!;
}
