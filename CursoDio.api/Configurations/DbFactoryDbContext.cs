using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoDio.api.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CursoDio.api.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer(@"Password=1234;Persist Security Info=True;User ID=sa;Initial Catalog=CursoDio;Data Source=DESKTOP-VDLI2RL\IVOSERVER");
            CursoDbContext context = new CursoDbContext(optionsBuilder.Options);
            return context;

        }
    }
}
