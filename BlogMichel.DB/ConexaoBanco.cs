﻿using BlogMichel.DB.Classes;
using BlogMichel.DB.Infra;
using BlogMichel.DB.Mapeamentos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMichel.DB
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

    public class conexaoBanco : DbContext
    {
        public conexaoBanco() : base("ConexaoMYSQL")
        {
            Database.Log = (p => Debug.WriteLine(p));
        }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Download> Downloads { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<TagPost> TagPosts { get; set; }
        public DbSet<TagClass> TagClass { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Visita> Visitas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer<conexaoBanco>(new MeuCriadorDeBanco());

            modelBuilder.Configurations.Add(new ArquivoConfig());
            modelBuilder.Configurations.Add(new ComentarioConfig());
            modelBuilder.Configurations.Add(new DownloadConfig());
            modelBuilder.Configurations.Add(new ImagemConfig());
            modelBuilder.Configurations.Add(new PostConfig());
            modelBuilder.Configurations.Add(new TagPostConfig());
            modelBuilder.Configurations.Add(new TagClassConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new VisitaConfig());

            base.OnModelCreating(modelBuilder);
        }

    }

}
