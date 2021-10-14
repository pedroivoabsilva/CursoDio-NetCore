using CursoDio.api.Business.Entities;
using CursoDio.api.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoDio.api.Infraestruture.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext _context;

        public UsuarioRepository(CursoDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.usuarios.Add(usuario);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Usuario ObterUsuario(string login)
        {
            return _context.usuarios.FirstOrDefault(u => u.Login == login);
        }
    }
}
