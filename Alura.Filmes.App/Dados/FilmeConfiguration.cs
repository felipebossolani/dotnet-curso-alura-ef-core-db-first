using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alura.Filmes.App.Dados
{
    public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder
                .ToTable("film");

            builder
                .Property(a => a.Id)
                .HasColumnName("film_id");

            builder
                .Property(a => a.Titulo)
                .HasColumnName("title")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder
                .Property(a => a.Descricao)
                .HasColumnName("description")
                .HasColumnType("text");

            builder
                .Property(a => a.AnoLancamento)
                .HasColumnName("release_year")
                .HasColumnType("varchar(4)");

            builder
                .Property(a => a.Duracao)
                .HasColumnName("length")
                .HasColumnType("smallint");

            builder
                .Property(a => a.Rating)
                .HasColumnName("rating")
                .HasColumnType("varchar(10)");

            builder
                .Property<DateTime>("last_update")
                .HasColumnName("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property<byte>("language_id");
            builder.Property<byte?>("original_language_id");

            builder
                .HasOne(f => f.IdiomaFalado)
                .WithMany(i => i.FilmesFalados)
                .HasForeignKey("language_id");

            builder
                .HasOne(f => f.IdiomaOriginal)
                .WithMany(i => i.FilmesOriginais)
                .HasForeignKey("original_language_id");

        }
    }
}
