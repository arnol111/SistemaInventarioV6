using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInventario.Modelos.Models;

namespace SistemaInventario.AccesoDatos.Configuracion;

public class BodegaConfiguracion : IEntityTypeConfiguration<Bodega>
{
    public void Configure(EntityTypeBuilder<Bodega> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(60);
        builder.Property(x=>x.Nombre).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Estado).IsRequired();
    }
}