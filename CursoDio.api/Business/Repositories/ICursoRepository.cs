using CursoDio.api.Business.Entities;
using System.Collections.Generic;

namespace CursoDio.api.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();
        IList<Curso> ObterPorUsuario(int codigoUsuario);
    }
}
