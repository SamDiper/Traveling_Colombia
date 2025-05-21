using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TravelingColombia.Models;

public partial class TravelingColombiabdContext : DbContext
{
    public TravelingColombiabdContext()
    {
    }

    public TravelingColombiabdContext(DbContextOptions<TravelingColombiabdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aerolinea> Aerolineas { get; set; }

    public virtual DbSet<Banco> Bancos { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Destino> Destinos { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<Hotele> Hoteles { get; set; }

    public virtual DbSet<Informe> Informes { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<TipoPlan> TipoPlans { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Viaje> Viajes { get; set; }

    public virtual DbSet<VuelosAerolinea> VuelosAerolineas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=TRAVELING_COLOMBIABD;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aerolinea>(entity =>
        {
            entity.HasKey(e => e.IdAerolinea).HasName("PK__Aeroline__E58B9C72D8BA12BD");

            entity.ToTable("Aerolinea");

            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.NombreAerolinea)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Aerolinea");
        });

        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.IdBanco).HasName("PK__Banco__37C5FC6173E01729");

            entity.ToTable("Banco");

            entity.Property(e => e.IdBanco).HasColumnName("Id_Banco");
            entity.Property(e => e.NombreBanco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Banco");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClases).HasName("PK__Clases__2D2D19BF62FA6371");

            entity.Property(e => e.IdClases).HasColumnName("Id_Clases");
            entity.Property(e => e.NombreClase)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Clase");
            entity.Property(e => e.PrecioClase)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Precio_Clase");
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.HasKey(e => e.IdDestino).HasName("PK__Destinos__CE218AE7CC43933A");

            entity.Property(e => e.IdDestino).HasColumnName("Id_Destino");
            entity.Property(e => e.NombreDestino)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Destino");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__AB2EB6F8D6E8FD8D");

            entity.ToTable("Estado");

            entity.Property(e => e.IdEstado).HasColumnName("Id_Estado");
            entity.Property(e => e.Estado1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Estado");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__A8B88C22F47F9069");

            entity.ToTable("Factura");

            entity.Property(e => e.IdFactura).HasColumnName("Id_Factura");
            entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaFactura).HasColumnName("Fecha_Factura");
            entity.Property(e => e.IdPago).HasColumnName("Id_Pago");
            entity.Property(e => e.IdReserva).HasColumnName("Id_Reserva");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__Id_Pago__6754599E");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__Id_Rese__66603565");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__6B8A72E2C66B09E8");

            entity.Property(e => e.IdHabitacion).HasColumnName("Id_Habitacion");
            entity.Property(e => e.CantidadHabitacion).HasColumnName("Cantidad_Habitacion");
            entity.Property(e => e.IdHotel).HasColumnName("Id_Hotel");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Tipo_Habitacion");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__Id_Ho__412EB0B6");
        });

        modelBuilder.Entity<Hotele>(entity =>
        {
            entity.HasKey(e => e.IdHotel).HasName("PK__Hoteles__40D3513510E1EDF1");

            entity.Property(e => e.IdHotel).HasColumnName("Id_Hotel");
            entity.Property(e => e.NombreHotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Hotel");
            entity.Property(e => e.UbicacionHotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Ubicacion_Hotel");
        });

        modelBuilder.Entity<Informe>(entity =>
        {
            entity.HasKey(e => e.IdInforme).HasName("PK__Informe__9369A0DD4BA386E8");

            entity.ToTable("Informe");

            entity.Property(e => e.IdInforme).HasColumnName("Id_Informe");
            entity.Property(e => e.IdFactura).HasColumnName("Id_Factura");
            entity.Property(e => e.IdPlan).HasColumnName("Id_Plan");
            entity.Property(e => e.IdReserva).HasColumnName("Id_Reserva");
            entity.Property(e => e.IdViaje).HasColumnName("Id_Viaje");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK__Informe__Id_Fact__6D0D32F4");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdPlan)
                .HasConstraintName("FK__Informe__Id_Plan__6A30C649");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK__Informe__Id_Rese__6C190EBB");

            entity.HasOne(d => d.IdViajeNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdViaje)
                .HasConstraintName("FK__Informe__Id_Viaj__6B24EA82");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodo).HasName("PK__Metodo_P__BDBEE834D2E77988");

            entity.ToTable("Metodo_Pagos");

            entity.Property(e => e.IdMetodo).HasColumnName("Id_Metodo");
            entity.Property(e => e.MetodoPago1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Metodo_Pago");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pagos__3E79AD9AD52F04A4");

            entity.Property(e => e.IdPago).HasColumnName("Id_Pago");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Cuenta)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdBanco).HasColumnName("Id_Banco");
            entity.Property(e => e.IdMetodo).HasColumnName("Id_Metodo");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBancoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdBanco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__Id_Banco__5CD6CB2B");

            entity.HasOne(d => d.IdMetodoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMetodo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__Id_Metodo__5DCAEF64");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.IdPlan).HasName("PK__Planes__3BD89AB6904DB57A");

            entity.Property(e => e.IdPlan).HasColumnName("Id_Plan");
            entity.Property(e => e.CantidadPersonas).HasColumnName("Cantidad_Personas");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FechaIda).HasColumnName("Fecha_Ida");
            entity.Property(e => e.FechaRegreso).HasColumnName("Fecha_Regreso");
            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.IdDestinoIda).HasColumnName("Id_Destino_Ida");
            entity.Property(e => e.IdHotel).HasColumnName("Id_Hotel");
            entity.Property(e => e.IdTipoPlan).HasColumnName("Id_Tipo_Plan");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NombrePlan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Plan");
            entity.Property(e => e.PrecioPlan)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Precio_Plan");

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.Planes)
                .HasForeignKey(d => d.IdAerolinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Planes__Id_Aerol__52593CB8");

            entity.HasOne(d => d.IdDestinoIdaNavigation).WithMany(p => p.Planes)
                .HasForeignKey(d => d.IdDestinoIda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Planes__Id_Desti__4F7CD00D");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.Planes)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Planes__Id_Hotel__5165187F");

            entity.HasOne(d => d.IdTipoPlanNavigation).WithMany(p => p.Planes)
                .HasForeignKey(d => d.IdTipoPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Planes__Id_Tipo___5070F446");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__9E953BE1A5C2CF40");

            entity.Property(e => e.IdReserva).HasColumnName("Id_Reserva");
            entity.Property(e => e.FechaReserva).HasColumnName("Fecha_Reserva");
            entity.Property(e => e.IdEstadoReserva).HasColumnName("Id_Estado_Reserva");
            entity.Property(e => e.IdPlan).HasColumnName("Id_Plan");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.IdViaje).HasColumnName("Id_Viaje");

            entity.HasOne(d => d.IdEstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadoReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__Id_Est__619B8048");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPlan)
                .HasConstraintName("FK__Reservas__Id_Pla__6383C8BA");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__Id_Usu__628FA481");

            entity.HasOne(d => d.IdViajeNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdViaje)
                .HasConstraintName("FK__Reservas__Id_Via__60A75C0F");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__55932E86A5C77162");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.Rol1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Rol");
        });

        modelBuilder.Entity<TipoPlan>(entity =>
        {
            entity.HasKey(e => e.IdTipoPlan).HasName("PK__Tipo_Pla__A24A0733E250924B");

            entity.ToTable("Tipo_Plan");

            entity.Property(e => e.IdTipoPlan).HasColumnName("Id_Tipo_Plan");
            entity.Property(e => e.NombrePlan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Plan");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__63C76BE2805FC9AC");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Apellido_Usuario");
            entity.Property(e => e.CantidadFacturas).HasColumnName("Cantidad_Facturas");
            entity.Property(e => e.CelularUsuario)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Celular_Usuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EdadUsuario).HasColumnName("Edad_Usuario");
            entity.Property(e => e.EmailUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Email_Usuario");
            entity.Property(e => e.IdRol).HasColumnName("Id_Rol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__Id_Rol__59FA5E80");
        });

        modelBuilder.Entity<Viaje>(entity =>
        {
            entity.HasKey(e => e.IdViaje).HasName("PK__Viajes__9BC209F794549882");

            entity.Property(e => e.IdViaje).HasColumnName("Id_Viaje");
            entity.Property(e => e.CantidadPuestos).HasColumnName("Cantidad_Puestos");
            entity.Property(e => e.FechaViaje).HasColumnName("Fecha_Viaje");
            entity.Property(e => e.HoraLlegada).HasColumnName("Hora_Llegada");
            entity.Property(e => e.HoraSalida).HasColumnName("Hora_Salida");
            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.IdDestinoIda).HasColumnName("Id_Destino_Ida");
            entity.Property(e => e.IdDestinoLlegada).HasColumnName("Id_Destino_Llegada");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PrecioViaje)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Precio_Viaje");

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.IdAerolinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__Id_Aerol__571DF1D5");

            entity.HasOne(d => d.IdDestinoIdaNavigation).WithMany(p => p.ViajeIdDestinoIdaNavigations)
                .HasForeignKey(d => d.IdDestinoIda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__Id_Desti__5535A963");

            entity.HasOne(d => d.IdDestinoLlegadaNavigation).WithMany(p => p.ViajeIdDestinoLlegadaNavigations)
                .HasForeignKey(d => d.IdDestinoLlegada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__Id_Desti__5629CD9C");
        });

        modelBuilder.Entity<VuelosAerolinea>(entity =>
        {
            entity.HasKey(e => e.IdVueloAerolinea).HasName("PK__VuelosAe__F52B80B2165B0CE3");

            entity.ToTable("VuelosAerolinea");

            entity.Property(e => e.IdVueloAerolinea).HasColumnName("Id_VueloAerolinea");
            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.IdDestino).HasColumnName("Id_Destino");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.VuelosAerolineas)
                .HasForeignKey(d => d.IdAerolinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VuelosAer__Id_Ae__4CA06362");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.VuelosAerolineas)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VuelosAer__Id_De__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
