using System;
using System.Collections.Generic;

namespace TravelingColombia.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ApellidoUsuario { get; set; } = null!;

    public string CelularUsuario { get; set; } = null!;

    public string EmailUsuario { get; set; } = null!;

    public int EdadUsuario { get; set; }

    public int IdRol { get; set; }

    public int CantidadFacturas { get; set; }

    public string Contrasena { get; set; } = null!;

    public string? FotoUsuario { get; set; }

    public string? CedulaUsuario { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
