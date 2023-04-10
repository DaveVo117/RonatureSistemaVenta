using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ronature.Entity;

namespace Ronature.DAL.DBContext
{
    public partial class RONATURE_DBContext : DbContext
    {
        public RONATURE_DBContext()
        {
        }

        public RONATURE_DBContext(DbContextOptions<RONATURE_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<Configuracion> Configuracions { get; set; } = null!;
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Negocio> Negocios { get; set; } = null!;
        public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolMenu> RolMenus { get; set; } = null!;
        public virtual DbSet<TipoDocumentoVenta> TipoDocumentoVenta { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Venta> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__02AA0785C7E27DC3");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion");
            });

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Configuracion");

                entity.Property(e => e.TxtPropiedad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Propiedad");

                entity.Property(e => e.TxtRecurso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Recurso");

                entity.Property(e => e.TxtValor)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Valor");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__DetalleV__DF908C88A8BEE0EF");

                entity.Property(e => e.IdDetalleVenta).HasColumnName("ID_Detalle_Venta");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TxtCategoriaProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Categoria_Producto");

                entity.Property(e => e.TxtDescripcionProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion_Producto");

                entity.Property(e => e.TxtMarcaProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Marca_Producto");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__ID_Ve__440B1D61");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Menu__C26AF48381C74087");

                entity.ToTable("Menu");

                entity.Property(e => e.IdMenu).HasColumnName("ID_Menu");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMenuPadre).HasColumnName("ID_Menu_Padre");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtControlador)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Controlador");

                entity.Property(e => e.TxtDescripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion");

                entity.Property(e => e.TxtIcono)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Icono");

                entity.Property(e => e.TxtPaginaAccion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Pagina_Accion");

                entity.HasOne(d => d.IdMenuPadreNavigation)
                    .WithMany(p => p.InverseIdMenuPadreNavigation)
                    .HasForeignKey(d => d.IdMenuPadre)
                    .HasConstraintName("FK__Menu__idMenuPadr__24927208");
            });

            modelBuilder.Entity<Negocio>(entity =>
            {
                entity.HasKey(e => e.IdNegocio)
                    .HasName("PK__Negocio__93197F13ECEDD59D");

                entity.ToTable("Negocio");

                entity.Property(e => e.IdNegocio)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Negocio");

                entity.Property(e => e.PorcentajeImpuesto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Porcentaje_Impuesto");

                entity.Property(e => e.TxtCorreo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Correo");

                entity.Property(e => e.TxtDireccion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Direccion");

                entity.Property(e => e.TxtNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre");

                entity.Property(e => e.TxtNombreLogo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre_Logo");

                entity.Property(e => e.TxtNumeroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Numero_Documento");

                entity.Property(e => e.TxtSimboloMoneda)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Simbolo_Moneda");

                entity.Property(e => e.TxtTelefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Telefono");

                entity.Property(e => e.TxtUrlLogo)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Url_Logo");
            });

            modelBuilder.Entity<NumeroCorrelativo>(entity =>
            {
                entity.HasKey(e => e.IdNumeroCorrelativo)
                    .HasName("PK__NumeroCo__DF757C85204C7106");

                entity.ToTable("NumeroCorrelativo");

                entity.Property(e => e.IdNumeroCorrelativo).HasColumnName("ID_Numero_Correlativo");

                entity.Property(e => e.CantidadDigitos).HasColumnName("Cantidad_Digitos");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Actualizacion");

                entity.Property(e => e.Gestion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UltimoNumero).HasColumnName("Ultimo_Numero");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__9B4120E279F902D4");

                entity.ToTable("Producto");

                entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtCodigoBarra)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Codigo_Barra");

                entity.Property(e => e.TxtDescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion");

                entity.Property(e => e.TxtMarca)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Marca");

                entity.Property(e => e.TxtNombreImagen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre_Imagen");

                entity.Property(e => e.TxtUrlImagen)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Url_Imagen");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Producto__ID_Cat__36B12243");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__202AD2200549CE6D");

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtDescripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion");
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.HasKey(e => e.IdRolMenu)
                    .HasName("PK__RolMenu__26D7077BFC818D15");

                entity.ToTable("RolMenu");

                entity.Property(e => e.IdRolMenu).HasColumnName("ID_Rol_Menu");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMenu).HasColumnName("ID_Menu");

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK__RolMenu__ID_Menu__2C3393D0");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__RolMenu__ID_Rol__2B3F6F97");
            });

            modelBuilder.Entity<TipoDocumentoVenta>(entity =>
            {
                entity.HasKey(e => e.IdTipoDocumentoVenta)
                    .HasName("PK__TipoDocu__8E1F174AC44D7B13");

                entity.Property(e => e.IdTipoDocumentoVenta).HasColumnName("ID_Tipo_Documento_Venta");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtDescripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Descripcion");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__DE4431C50DBED38F");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdRol).HasColumnName("ID_Rol");

                entity.Property(e => e.SnActivo).HasColumnName("SN_Activo");

                entity.Property(e => e.TxtClave)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Clave");

                entity.Property(e => e.TxtCorreo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Correo");

                entity.Property(e => e.TxtNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre");

                entity.Property(e => e.TxtNombreFoto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre_Foto");

                entity.Property(e => e.TxtTelefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Telefono");

                entity.Property(e => e.TxtUrlFoto)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Url_Foto");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuario__ID_Rol__300424B4");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__3CD842E52E4C125F");

                entity.Property(e => e.IdVenta).HasColumnName("ID_Venta");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Registro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdTipoDocumentoVenta).HasColumnName("ID_Tipo_Documento_Venta");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.Property(e => e.ImpuestoTotal)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Impuesto_Total");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TxtDocumentoCliente)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Documento_Cliente");

                entity.Property(e => e.TxtNombreCliente)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Nombre_Cliente");

                entity.Property(e => e.TxtNumeroVenta)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Numero_Venta");

                entity.HasOne(d => d.IdTipoDocumentoVentaNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdTipoDocumentoVenta)
                    .HasConstraintName("FK__Venta__ID_Tipo_D__3F466844");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Venta__ID_Usuari__403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
