using CursoDio.api.Business.Entities;
using CursoDio.api.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoDio.api.Infraestruture.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _context;

        public CursoRepository(CursoDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Curso curso)
        {
            _context.cursos.Add(curso);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IList<Curso> ObterPorUsuario(int codigoUsuario)
        {
            return _context.cursos.Include(i => i.Usuario).Where(w => w.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}
