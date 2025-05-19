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



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aerolinea>(entity =>
        {
            entity.HasKey(e => e.IdAerolinea).HasName("PK__Aeroline__E58B9C722A0E98E7");

            entity.ToTable("Aerolinea");

            entity.Property(e => e.IdAerolinea)
                .ValueGeneratedNever()
                .HasColumnName("Id_Aerolinea");
            entity.Property(e => e.NombreAerolinea)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Aerolinea");
        });

        modelBuilder.Entity<Banco>(entity =>
        {
            entity.HasKey(e => e.IdBanco).HasName("PK__Banco__37C5FC610F717499");

            entity.ToTable("Banco");

            entity.Property(e => e.IdBanco)
                .ValueGeneratedNever()
                .HasColumnName("Id_Banco");
            entity.Property(e => e.NombreBanco)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Banco");
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.HasKey(e => e.IdClases).HasName("PK__Clases__2D2D19BF91AB2E33");

            entity.Property(e => e.IdClases)
                .ValueGeneratedNever()
                .HasColumnName("Id_Clases");
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
            entity.HasKey(e => e.IdDestino).HasName("PK__Destinos__CE218AE7BB5E48D5");

            entity.Property(e => e.IdDestino)
                .ValueGeneratedNever()
                .HasColumnName("Id_Destino");
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
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__AB2EB6F8AE1518AB");

            entity.ToTable("Estado");

            entity.Property(e => e.IdEstado)
                .ValueGeneratedNever()
                .HasColumnName("Id_Estado");
            entity.Property(e => e.Estado1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Estado");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__A8B88C22BA9E7C81");

            entity.ToTable("Factura");

            entity.Property(e => e.IdFactura)
                .ValueGeneratedNever()
                .HasColumnName("Id_Factura");
            entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaPago).HasColumnName("Fecha_Pago");
            entity.Property(e => e.IdPago).HasColumnName("Id_Pago");
            entity.Property(e => e.IdReserva).HasColumnName("Id_Reserva");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Pago");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Reserva");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__6B8A72E2A7B9C16B");

            entity.Property(e => e.IdHabitacion)
                .ValueGeneratedNever()
                .HasColumnName("Id_Habitacion");
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
                .HasConstraintName("FK_Habitacion_Hotel");
        });

        modelBuilder.Entity<Hotele>(entity =>
        {
            entity.HasKey(e => e.IdHotel).HasName("PK__Hoteles__40D351357E600FD2");

            entity.Property(e => e.IdHotel)
                .ValueGeneratedNever()
                .HasColumnName("Id_Hotel");
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
            entity.HasKey(e => e.IdInforme).HasName("PK__Informe__9369A0DD97706A87");

            entity.ToTable("Informe");

            entity.Property(e => e.IdInforme)
                .ValueGeneratedNever()
                .HasColumnName("Id_Informe");
            entity.Property(e => e.IdFactura).HasColumnName("Id_Factura");
            entity.Property(e => e.IdPlan).HasColumnName("Id_Plan");
            entity.Property(e => e.IdReserva).HasColumnName("Id_Reserva");
            entity.Property(e => e.IdViaje).HasColumnName("Id_Viaje");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_Informe_Factura");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdPlan)
                .HasConstraintName("FK_Informe_Plan");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_Informe_Reserva");

            entity.HasOne(d => d.IdViajeNavigation).WithMany(p => p.Informes)
                .HasForeignKey(d => d.IdViaje)
                .HasConstraintName("FK_Informe_Viaje");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodo).HasName("PK__Metodo_P__BDBEE834F8330B86");

            entity.ToTable("Metodo_Pagos");

            entity.Property(e => e.IdMetodo)
                .ValueGeneratedNever()
                .HasColumnName("Id_Metodo");
            entity.Property(e => e.MetodoPago1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Metodo_Pago");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pagos__3E79AD9A8134CE38");

            entity.Property(e => e.IdPago)
                .ValueGeneratedNever()
                .HasColumnName("Id_Pago");
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
                .HasConstraintName("FK_Pago_Banco");

            entity.HasOne(d => d.IdMetodoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMetodo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Metodo");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.HasKey(e => e.IdPlan).HasName("PK__Planes__3BD89AB66BFC5B1C");

            entity.Property(e => e.IdPlan)
                .ValueGeneratedNever()
                .HasColumnName("Id_Plan");
            entity.Property(e => e.CantidadPersonas).HasColumnName("Cantidad_Personas");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.IdClase).HasColumnName("Id_Clase");
            entity.Property(e => e.IdDestinoIda).HasColumnName("Id_Destino_Ida");
            entity.Property(e => e.IdDestinoLlegada).HasColumnName("Id_Destino_Llegada");
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
                .HasConstraintName("FK_Plan_Aerolinea");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.Planes)
                .HasForeignKey(d => d.IdClase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plan_Clase");

            entity.HasOne(d => d.IdDestinoIdaNavigation).WithMany(p => p.PlaneIdDestinoIdaNavigations)
                .HasForeignKey(d => d.IdDestinoIda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plan_DestinoIda");

            entity.HasOne(d => d.IdDestinoLlegadaNavigation).WithMany(p => p.PlaneIdDestinoLlegadaNavigations)
                .HasForeignKey(d => d.IdDestinoLlegada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plan_DestinoLlegada");

            entity.HasOne(d => d.IdHotelNavigation).WithMany(p => p.Planes)
                .HasForeignKey(d => d.IdHotel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plan_Hotel");

            entity.HasOne(d => d.IdTipoPlanNavigation).WithMany(p => p.Planes)
                .HasForeignKey(d => d.IdTipoPlan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plan_Tipo");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__9E953BE15E96BDFF");

            entity.Property(e => e.IdReserva)
                .ValueGeneratedNever()
                .HasColumnName("Id_Reserva");
            entity.Property(e => e.EstadoReserva).HasColumnName("Estado_Reserva");
            entity.Property(e => e.FechaReserva).HasColumnName("Fecha_Reserva");
            entity.Property(e => e.IdPlan).HasColumnName("Id_Plan");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.IdViaje).HasColumnName("Id_Viaje");

            entity.HasOne(d => d.EstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.EstadoReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Estado");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPlan)
                .HasConstraintName("FK_Reserva_Plan");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Usuario");

            entity.HasOne(d => d.IdViajeNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdViaje)
                .HasConstraintName("FK_Reserva_Viaje");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__55932E86B4677FEF");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("Id_Rol");
            entity.Property(e => e.Rol1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Rol");
        });

        modelBuilder.Entity<TipoPlan>(entity =>
        {
            entity.HasKey(e => e.IdTipoPlan).HasName("PK__Tipo_Pla__A24A07331482B435");

            entity.ToTable("Tipo_Plan");

            entity.Property(e => e.IdTipoPlan)
                .ValueGeneratedNever()
                .HasColumnName("Id_Tipo_Plan");
            entity.Property(e => e.NombrePlan)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Plan");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__63C76BE2877AB2EA");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("Id_Usuario");
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
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<Viaje>(entity =>
        {
            entity.HasKey(e => e.IdViaje).HasName("PK__Viajes__9BC209F79C6A5E3B");

            entity.Property(e => e.IdViaje)
                .ValueGeneratedNever()
                .HasColumnName("Id_Viaje");
            entity.Property(e => e.CantidadPuestos).HasColumnName("Cantidad_Puestos");
            entity.Property(e => e.FechaViaje).HasColumnName("Fecha_Viaje");
            entity.Property(e => e.HoraLlegada).HasColumnName("Hora_Llegada");
            entity.Property(e => e.HoraSalida).HasColumnName("Hora_Salida");
            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.IdClase).HasColumnName("Id_Clase");
            entity.Property(e => e.IdDestino).HasColumnName("Id_Destino");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PrecioViaje)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Precio_Viaje");

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.IdAerolinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viaje_Aerolinea");

            entity.HasOne(d => d.IdClaseNavigation).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.IdClase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viaje_Clase");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viaje_Destino");
        });

        modelBuilder.Entity<VuelosAerolinea>(entity =>
        {
            entity.HasKey(e => e.IdVueloAerolinea).HasName("PK__VuelosAe__F52B80B29DA47346");

            entity.ToTable("VuelosAerolinea");

            entity.Property(e => e.IdVueloAerolinea)
                .ValueGeneratedNever()
                .HasColumnName("Id_VueloAerolinea");
            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.IdDestino).HasColumnName("Id_Destino");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.VuelosAerolineas)
                .HasForeignKey(d => d.IdAerolinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VueloAerolinea_Aerolinea");

            entity.HasOne(d => d.IdDestinoNavigation).WithMany(p => p.VuelosAerolineas)
                .HasForeignKey(d => d.IdDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VueloAerolinea_Destino");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
