using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInventario.Modelos.Models;

namespace SistemaInventario.AccesoDatos.Configuracion;

public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
{
    public void Configure(EntityTypeBuilder<Marca> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
        builder.Property(x=>x.Descripcion).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Estado).IsRequired();
        builder.HasIndex(x => x.Id).IsUnique();
    }
}