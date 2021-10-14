using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoDio.api.Business.Entities;
using CursoDio.api.Infraestruture.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CursoDio.api.Infraestruture.Data
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options):base(options)
        {
              
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Curso> cursos { get; set; }

    }
}
