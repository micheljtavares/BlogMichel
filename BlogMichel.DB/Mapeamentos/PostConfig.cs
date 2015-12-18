using BlogMichel.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMichel.DB.Mapeamentos
{
    public class PostConfig : EntityTypeConfiguration<Post>
    {
        public PostConfig()
        {
            ToTable("POST");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Autor)
                .HasColumnName("AUTOR")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.DataPublicacao)
               .HasColumnName("DATAPUBLICAÇAO")
               .IsRequired();

            Property(x => x.Descricao)
               .HasColumnName("DESCRIÇAO")
               .IsMaxLength()
               .IsRequired();

            Property(x => x.Resumo)
               .HasColumnName("RESUMO")
               .HasMaxLength(1000)
               .IsRequired();

            Property(x => x.Titulo)
               .HasColumnName("TITULO")
               .HasMaxLength(100)
               .IsRequired();

            Property(x => x.Visivel)
               .HasColumnName("VISIVEL")
               .IsRequired();

            HasMany(x => x.Arquivo)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            HasMany(x => x.Comentarios)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            HasMany(x => x.Imagem)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            HasMany(x => x.Visita)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);

            HasMany(x => x.TagsPost)
                .WithOptional()
                .HasForeignKey(x => x.IdPost);



        }

    }
}
